using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;

namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductByIdQuery : IRequest<Result<GetProductByIdResponse>>
    {
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
