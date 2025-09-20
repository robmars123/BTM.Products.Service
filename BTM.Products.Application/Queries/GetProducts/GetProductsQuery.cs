using BTM.Products.Application.Abstractions;
using BTM.Products.Contracts.ProductDTOs;

namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
