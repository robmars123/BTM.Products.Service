using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Commands;
using BTM.Products.Contracts.ProductCommands;

namespace BTM.Products.Api.Endpoints.Create
{
    public class CreateProductEndpoints
    {
        private readonly ITokenService _tokenService;

        public CreateProductEndpoints(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public static async Task<IResult> Create(CreateProductRequest product, IDispatcher dispatcher)
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
