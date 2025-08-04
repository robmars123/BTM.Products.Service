using BTM.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTM.Products.Infrastructure.Connection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) // This passes the options to the base constructor of DbContext
        {
            
        }

        public DbSet<Product> Products { get; set; } = null!; // Ensure Products is initialized
    }
}
