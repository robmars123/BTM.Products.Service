using BTM.Products.Api.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace BTM.Products.Api.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/products", ProductEndpoints.GetProduct).RequireAuthorization();
            endpoints.MapPost("/api/products", ProductEndpoints.CreateProduct).RequireAuthorization();
            endpoints.MapGet("/token", ([FromServices] ProductEndpoints endpoints) => endpoints.RequestToken());

            return endpoints;
        }
    }

}
