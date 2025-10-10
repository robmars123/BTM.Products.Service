using BTM.Products.Api.Factories.Abstractions;
using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Queries.GetAllProducts;
using BTM.Products.Application.Queries.GetProducts;
using BTM.Products.Application.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BTM.Products.Api.Endpoints.GetById
{
    public class GetAllProductsEndpoint
    {
        private readonly ITokenService _tokenService;
        public GetAllProductsEndpoint(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public static async Task<IResult> GetPagedProducts(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            IDispatcher dispatcher,
            [FromServices] IGetAllProductsFactory factory,
            CancellationToken cancellationToken)
        {
            // Ensure valid input
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            GetPagedProductsQuery query = new GetPagedProductsQuery(page, pageSize);
            var result = await dispatcher.Send<GetPagedProductsQuery, Result<List<GetAllProductsResponse>>>(query,cancellationToken);

            if (!result.IsSuccess || result.Data == null)
                return Results.NotFound(result.ErrorMessage);

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
