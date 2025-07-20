using BTM.Products.Application.Abstractions;
using BTM.Products.Contracts.ProductDTOs;
using BTM.Products.Domain.Entities;

namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request)
        {
            return new List<ProductDto>() { new ProductDto() { Id = 1, Name = "Product 1", Price = 10.0m } };
        }
    }
}
