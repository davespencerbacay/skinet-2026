using Core.Entities;
using Core.Interfaces;

namespace Infrastracture.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<ShoppingCart> CreateOrUpdatePaymentIntent(string cartId)
        {
            throw new NotImplementedException();
        }
    }
}