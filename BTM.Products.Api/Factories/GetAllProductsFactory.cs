using BTM.Products.Api.Factories.Abstractions;
using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Queries.GetAllProducts;

namespace BTM.Products.Api.Factories
{
    public class GetAllProductsFactory : IGetAllProductsFactory
    {
        public ProductResponse Create(GetAllProductsResponse source)
        {
            if (source == null) return null;
            return new ProductResponse(source.Id, source.Name, source.UnitPrice);
        }

        public IEnumerable<ProductResponse> Create(IEnumerable<GetAllProductsResponse> source)
        {
            if (source == null) return Enumerable.Empty<ProductResponse>();

            return source.Select(Create);
        }
    }
}
