using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Queries.GetAllProducts;
using BTM.Products.Application.Queries.GetProducts;
using BTM.Products.Application.Results;
using BTM.Products.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BTM.Products.Application.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<GetAllProductsResponse>>>
    {
        private readonly string _connectionString;

        public GetAllProductsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<Result<List<GetAllProductsResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = """
                        SELECT Id, Name, UnitPrice
                        FROM Product
                        WHERE IsDeleted = 0
                        """;

            List<Product> products = (await connection.QueryAsync<Product>(sql)).ToList();
             
            if (!products.Any())
                return Result<List<GetAllProductsResponse>>.Failure("No products found matching the criteria.");

            List<GetAllProductsResponse> getProductResponse = products.Select(prod => new GetAllProductsResponse(prod.Id, prod.Name, prod.UnitPrice)).ToList();

            return Result<List<GetAllProductsResponse>>.Success(getProductResponse);
        }
    }
}
