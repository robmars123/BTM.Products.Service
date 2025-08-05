using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;

namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<Result<List<GetProductResponse>>>
    {
        public GetProductsQuery(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}
