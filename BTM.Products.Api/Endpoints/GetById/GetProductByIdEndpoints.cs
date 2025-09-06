using BTM.Products.Api.Factories.Abstractions;
using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Queries.GetProducts;
using BTM.Products.Application.Results;

namespace BTM.Products.Api.Endpoints.GetById
{
    public class GetProductByIdEndpoints
    {
        private readonly ITokenService _tokenService;

        public GetProductByIdEndpoints(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public static async Task<IResult> GetProduct(Guid id, IDispatcher dispatcher, IGetProductByIdFactory factory)
        {
            var query = new GetProductByIdQuery(id);

            var result = await dispatcher.Send<GetProductByIdQuery, Result<List<GetProductByIdResponse>>>(query, CancellationToken.None);

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
