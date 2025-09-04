using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;

namespace BTM.Products.Application.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<Result<List<GetAllProductsResponse>>>
    {
        public GetAllProductsQuery(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}
