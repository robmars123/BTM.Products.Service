using BTM.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTM.Products.Infrastructure.Connection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } // Ensure Products is initialized

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.UnitPrice)
                      .HasColumnType("decimal(18,4)"); // <-- set precision here
            });

            modelBuilder.Entity<Product>().ToTable("Product");
        }
    }
}
