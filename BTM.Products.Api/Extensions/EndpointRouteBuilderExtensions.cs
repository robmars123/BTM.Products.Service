using BTM.Products.Api.Endpoints.Create;
using BTM.Products.Api.Endpoints.GetById;
using Microsoft.AspNetCore.Mvc;

namespace BTM.Products.Api.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/products", GetProductEndpoints.GetProduct).RequireAuthorization();
            endpoints.MapPost("/api/products", CreateProductEndpoints.Create).RequireAuthorization();
            endpoints.MapGet("/token", ([FromServices] CreateProductEndpoints endpoints) => endpoints.RequestToken());

            return endpoints;
        }
    }

}
