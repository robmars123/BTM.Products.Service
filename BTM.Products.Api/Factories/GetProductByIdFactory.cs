using BTM.Products.Api.Factories.Abstractions;
using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Queries.GetProducts;

namespace BTM.Products.Api.Factories
{
    public class GetProductByIdFactory : IGetProductByIdFactory
    {
        public ProductResponse Create(GetProductByIdResponse source)
        {
            if (source == null) return null;
            return new ProductResponse(source.Id, source.Name, source.UnitPrice);
        }

        public IEnumerable<ProductResponse> Create(IEnumerable<GetProductByIdResponse> source)
        {
            if (source == null) return Enumerable.Empty<ProductResponse>();

            return source.Select(Create);
        }
    }
}
