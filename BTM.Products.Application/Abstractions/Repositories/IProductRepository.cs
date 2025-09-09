using BTM.Products.Domain.Entities;

namespace BTM.Products.Application.Abstractions.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);  
    }
}
