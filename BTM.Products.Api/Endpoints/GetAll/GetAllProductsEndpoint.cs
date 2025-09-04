using BTM.Products.Api.Factories.Abstractions;
using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Queries.GetAllProducts;
using BTM.Products.Application.Queries.GetProducts;
using BTM.Products.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace BTM.Products.Api.Endpoints.GetById
{
    public class GetAllProductsEndpoint
    {
        private readonly ITokenService _tokenService;

        public GetAllProductsEndpoint(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public static async Task<IResult> GetProducts(IDispatcher dispatcher, [FromServices] IGetAllProductsFactory factory)
        {
            var result = await dispatcher.Send<GetAllProductsQuery, Result<List<GetAllProductsResponse>>>(new GetAllProductsQuery(), CancellationToken.None);

            if (result == null || !result.IsSuccess)
                return Results.NotFound(result?.ErrorMessage);

            IEnumerable<ProductResponse> response = factory.Create(result.Data);
            return Results.Ok(response);
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
