using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;

namespace BTM.Products.Application.Queries.GetAllProducts
{
    public record GetPagedProductsQuery(int page, int pageSize) : IRequest<Result<PagedResult<GetAllProductsResponse>>>;
}
