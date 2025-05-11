using BTM.Products.Application.Abstractions;
using BTM.Products.Domain.Entities;

namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
