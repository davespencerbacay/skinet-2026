using System.Text.Json;
using Core.Entities;

namespace Infrastracture.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.Products.Any())
            {
                var productsPath = Path.Combine("..", "Infrastracture", "Data", "SeedData", "products.json");
                var productsdata = await File.ReadAllTextAsync(productsPath);
                var products = JsonSerializer.Deserialize<List<Product>>(productsdata);

                if (products == null) return;

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

            if (!context.DeliveryMethods.Any())
            {
                var dmData = Path.Combine("..", "Infrastracture", "Data", "SeedData", "delivery.json");
                var deliveriesData = await File.ReadAllTextAsync(dmData);
                var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveriesData);

                if (deliveries == null) return;

                context.DeliveryMethods.AddRange(deliveries);
                await context.SaveChangesAsync();
            }
        }


    }
}