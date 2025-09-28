using BTM.Products.Api.Endpoints.Create;
using BTM.Products.Api.Endpoints.GetById;
using Microsoft.AspNetCore.Mvc;

namespace BTM.Products.Api.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/allproducts", GetAllProductsEndpoint.GetProducts);
            endpoints.MapGet("/api/getproduct", GetProductByIdEndpoints.GetProduct);
            endpoints.MapDelete("/api/removeproducts", RemoveProductByIdEndpoints.RemoveProduct).RequireAuthorization();
            endpoints.MapPost("/api/addproducts", CreateProductEndpoints.Create).RequireAuthorization();
            endpoints.MapPut("/api/updateproducts", UpdateProductEndpoints.Update).RequireAuthorization();
            endpoints.MapGet("/token", ([FromServices] GetAllProductsEndpoint endpoints) => endpoints.RequestToken());

            return endpoints;
        }
    }

}
