using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;
using BTM.Products.Contracts.ProductDTOs;

namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<Result<List<ProductResponse>>>
    {
        public GetProductsQuery(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}
