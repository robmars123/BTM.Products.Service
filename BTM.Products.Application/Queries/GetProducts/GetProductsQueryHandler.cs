using BTM.Products.Application.Abstractions;
using BTM.Products.Domain.Entities;

namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request)
        {
            return new List<Product>() { new Product() { Id = 1, Name = "Product 1", Price = 10.0m } };
        }
    }
}
