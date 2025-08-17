using BTM.Products.Api.Factories.Abstractions;
using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Commands;
using BTM.Products.Application.Queries.GetProducts;
using BTM.Products.Application.Results;
using BTM.Products.Contracts.ProductCommands;

namespace BTM.Products.Api.Endpoints
{
    public class ProductEndpoints
    {
        private readonly ITokenService _tokenService;

        public ProductEndpoints(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public static async Task<IResult> GetProduct(int id, IDispatcher dispatcher, IProductFactory factory)
        {
            var query = new GetProductsQuery(id);
            var result = await dispatcher.Send<GetProductsQuery, Result<List<GetProductResponse>>>(query, CancellationToken.None);

            if (result == null || !result.IsSuccess)
                return Results.NotFound(result?.ErrorMessage);

            var response = factory.Create(result.Data);
            return Results.Ok(response);
        }

        public static async Task<IResult> CreateProduct(CreateProductRequest product, IDispatcher dispatcher)
        {
            if (product == null)
                return Results.BadRequest();

            var command = new AddProductCommand(product.Name, product.Price);
            await dispatcher.Send(command);

            return Results.Created($"/api/products", product);
        }

        public async Task<IResult> RequestToken()
        {
            var result = await _tokenService.RequestClientCredentialsTokenAsync();

            return result.Success
                ? Results.Ok(result.AccessToken)
                : Results.Problem(result.ErrorMessage);
        }
    }

}
