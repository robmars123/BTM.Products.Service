using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Queries.GetProducts;

namespace BTM.Products.Api.Factories.Abstractions
{
    public interface IRemoveProductByIdFactory : IMapper<GetProductByIdResponse, ProductResponse>
    {
        new ProductResponse Create(GetProductByIdResponse source);
        new IEnumerable<ProductResponse> Create(IEnumerable<GetProductByIdResponse> source);
    }
}