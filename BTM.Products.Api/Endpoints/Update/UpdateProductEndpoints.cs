using BTM.Products.ApiClient.In;
using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Commands.AddProduct;

namespace BTM.Products.Api.Endpoints.Create
{
    public class UpdateProductEndpoints
    {
        private readonly ITokenService _tokenService;

        public UpdateProductEndpoints(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public static async Task<IResult> Update(UpdateProductRequest product, IDispatcher dispatcher)
        {
            if (product == null)
                return Results.BadRequest();

            var command = new UpdateProductCommand(product.id, product.name, product.unitPrice, product.isDeleted);
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
