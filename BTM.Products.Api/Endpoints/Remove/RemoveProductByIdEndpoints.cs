using BTM.Products.Api.Factories.Abstractions;
using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Commands.AddProduct;
using BTM.Products.Application.Commands.DeleteProduct;
using BTM.Products.Application.Queries.GetProducts;
using BTM.Products.Application.Results;
using BTM.Products.Domain.Entities;

namespace BTM.Products.Api.Endpoints.GetById
{
    public class RemoveProductByIdEndpoints
    {
        private readonly ITokenService _tokenService;

        public RemoveProductByIdEndpoints(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public static async Task<IResult> RemoveProduct(Guid id, IDispatcher dispatcher, IGetProductByIdFactory factory)
        {
            if(id == Guid.Empty)
                return Results.BadRequest();

            var command = new RemoveProductCommand(id);
            await dispatcher.Send(command);

            return Results.Ok(id);
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
