using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Data
{
    public class StoreContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}