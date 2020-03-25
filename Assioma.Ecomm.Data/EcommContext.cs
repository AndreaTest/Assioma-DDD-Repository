using Microsoft.EntityFrameworkCore;
using Assioma.Ecomm.Domain;

namespace Assioma.Ecomm.Data
{
    /// <summary>
    /// This is the real context we will use.
    /// </summary>
    public class EcommContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public EcommContext(DbContextOptions<EcommContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Product>().ToTable("Product");
            //modelBuilder.Entity<Order>().ToTable("Order");
            //modelBuilder.Entity<OrderProduct>().ToTable("OrderProduct");

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);
        }

        public void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
