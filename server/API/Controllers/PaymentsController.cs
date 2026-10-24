
using API.Extensions;
using API.SignalR;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Stripe;

namespace API.Controllers
{
    public class PaymentsController(IPaymentService paymentService, IUnitOfWork unit, ILogger<PaymentsController> logger, IConfiguration config, IHostEnvironment env, IHubContext<NotificationHub> hubContext) : BaseApiController
    {
        private readonly string? _whSecret = config["StripeSettings:WhSecret"];
        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
        {
            var cart = await paymentService.CreateOrUpdatePaymentIntent(cartId);
            if (cart == null) return BadRequest("Problem with your cart.");
            return Ok(cart);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await unit.Repository<DeliveryMethod>().ListAllAsync());
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = ConstructStripeEvent(json);

                if (stripeEvent.Type != "payment_intent.succeeded")
                {
                    return Ok();
                }

                if (stripeEvent.Data.Object is not PaymentIntent intent)
                {
                    logger.LogWarning("Stripe Webhook | Event payload could not be deserialized to PaymentIntent. Type: {EventType}", stripeEvent.Type);
                    return Ok();
                }

                await HandlePaymentIntentSucceeded(intent);
                return Ok();
            }
            catch (StripeException ex)
            {
                logger.LogWarning(ex, "Stripe Webhook | Signature validation failed.");

                if (env.IsDevelopment())
                {
                    return Ok();
                }

                return BadRequest("Webhook signature validation failed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occured.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occured.");
            }
        }

        private async Task HandlePaymentIntentSucceeded(PaymentIntent intent)
        {
            if (intent.Status == "succeeded")
            {
                var spec = new OrderSpecification(intent.Id, true);
                var order = await unit.Repository<Core.Entities.OrderAggregate.Order>().GetEntityWithSpec(spec);

                if (order == null)
                {
                    logger.LogWarning("Stripe Webhook | No order found for PaymentIntentId: {PaymentIntentId}", intent.Id);
                    return;
                }

                if ((long)order.GetTotal() * 100 != intent.Amount)
                {
                    order.Status = OrderStatus.PaymentMismatch;
                }
                else
                {
                    order.Status = OrderStatus.PaymentReceived;
                }

                await unit.Complete();

                var connectionId = NotificationHub.GetConnectionIdByEmail(order.BuyerEmail);

                if (!string.IsNullOrEmpty(connectionId))
                {
                    await hubContext.Clients.Client(connectionId).SendAsync("OrderCompleteNotification", order.ToDto());
                }
            }
        }

        private Event ConstructStripeEvent(string json)
        {
            if (string.IsNullOrWhiteSpace(_whSecret))
            {
                throw new StripeException("Stripe webhook secret is not configured.");
            }

            try
            {
                return EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret, throwOnApiVersionMismatch: false);
            }
            catch (Exception ex)
            {
                if (env.IsDevelopment())
                {
                    logger.LogWarning(ex, "Stripe Webhook | Signature validation failed in Development. Falling back to unsigned event parsing.");
                    return EventUtility.ParseEvent(json, throwOnApiVersionMismatch: false);
                }

                logger.LogError(ex, "Failed to construct stripe event.");
                throw new StripeException("Invalid Signature");
            }
        }
    }
}