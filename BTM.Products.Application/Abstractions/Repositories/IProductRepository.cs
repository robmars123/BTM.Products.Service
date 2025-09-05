using BTM.Products.Domain.Entities;

namespace BTM.Products.Application.Abstractions.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product, CancellationToken cancellationToken = default);
        void Remove(Product product, CancellationToken cancellationToken = default);  
    }
}
