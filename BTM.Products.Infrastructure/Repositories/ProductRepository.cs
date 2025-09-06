using BTM.Products.Application.Abstractions.Repositories;
using BTM.Products.Domain.Entities;
using BTM.Products.Infrastructure.Connection;

namespace BTM.Products.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            await AddAsync(product, cancellationToken);
        }
    }
}
