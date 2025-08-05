using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Queries.GetProducts;

namespace BTM.Products.Api.Factories.Abstractions
{
    public interface IProductFactory : IMapper<GetProductResponse, ProductResponse>
    {
        new ProductResponse Create(GetProductResponse source);
        new IEnumerable<ProductResponse> Create(IEnumerable<GetProductResponse> source);
    }
}