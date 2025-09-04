using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;

namespace BTM.Products.Application.Queries.GetAllProducts
{
    public record GetAllProductsQuery() : IRequest<Result<List<GetAllProductsResponse>>>;
}
