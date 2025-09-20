using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Queries.GetAllProducts;
using BTM.Products.Application.Queries.GetProducts;

namespace BTM.Products.Api.Factories.Abstractions
{
    public interface IGetAllProductsFactory : IMapper<GetAllProductsResponse, ProductResponse>
    {
        new ProductResponse Create(GetAllProductsResponse source);
        new IEnumerable<ProductResponse> Create(IEnumerable<GetAllProductsResponse> source);
    }
}