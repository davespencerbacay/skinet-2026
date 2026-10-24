using System.Reflection;
using System.Text.Json;
using Core.Entities;

namespace Infrastracture.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!context.Products.Any())
            {
                var productsdata = await File.ReadAllTextAsync(path + @"/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsdata);

                if (products == null) return;

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

            if (!context.DeliveryMethods.Any())
            {
                var deliveriesData = await File.ReadAllTextAsync(path + @"/Data/SeedData/delivery.json");
                var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveriesData);

                if (deliveries == null) return;

                context.DeliveryMethods.AddRange(deliveries);
                await context.SaveChangesAsync();
            }
        }


    }
}