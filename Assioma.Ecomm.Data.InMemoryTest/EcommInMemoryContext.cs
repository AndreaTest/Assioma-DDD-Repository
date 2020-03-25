using Microsoft.EntityFrameworkCore;
using Assioma.Ecomm.Data.InMemoryTest;
using Assioma.Ecomm.Domain;

namespace Assioma.Ecomm.Data.InMemoryTest
{
    public class EcommInMemoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public EcommInMemoryContext(DbContextOptions<EcommInMemoryContext> options) : base(options)
        {
            Database.EnsureCreated();

            // In memory we do not need async
            if (!Orders.AnyAsync().Result)
            {
                SeedData.Initialize(this);
            }
        }
    }
}
