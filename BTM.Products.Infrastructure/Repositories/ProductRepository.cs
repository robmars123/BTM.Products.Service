using BTM.Products.Application.Abstractions.Repositories;
using BTM.Products.Domain.Entities;
using BTM.Products.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

namespace BTM.Products.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Product> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            var product = await DbContext.Products.FirstOrDefaultAsync(p => p.Id == guid, cancellationToken);
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            return product;
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            await AddAsync(product, cancellationToken);
        }


        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await DbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            product.Remove();
        }
    }
}
