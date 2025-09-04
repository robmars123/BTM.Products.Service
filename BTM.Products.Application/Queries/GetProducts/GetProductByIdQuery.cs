using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;

namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductByIdQuery : IRequest<Result<List<GetProductByIdResponse>>>
    {
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}
