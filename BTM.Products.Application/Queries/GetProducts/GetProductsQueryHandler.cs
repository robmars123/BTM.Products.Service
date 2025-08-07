using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;
using BTM.Products.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<List<GetProductResponse>>>
    {
        private readonly string _connectionString;

        public GetProductsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<Result<List<GetProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return Result<List<GetProductResponse>>.Failure("test");
            }

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = """
                        SELECT Id, Name, UnitPrice
                        FROM Product
                        WHERE (@Id IS NULL OR Id = @Id)
                        """;

            var parameters = new
            {
                Id = request.Id
            };

            List<Product> products = (await connection.QueryAsync<Product>(sql, parameters)).ToList();

            if (!products.Any())
                return Result<List<GetProductResponse>>.Failure("No products found matching the criteria.");

            List<GetProductResponse> getProductResponse = products.Select(prod => new GetProductResponse(prod.Id, prod.Name, prod.UnitPrice)).ToList();

            return Result<List<GetProductResponse>>.Success(getProductResponse);
        }
    }
}
