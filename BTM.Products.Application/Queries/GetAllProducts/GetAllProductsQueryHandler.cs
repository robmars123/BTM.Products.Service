using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Results;
using BTM.Products.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BTM.Products.Application.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetPagedProductsQuery, Result<PagedResult<GetAllProductsResponse>>>
    {
        private readonly string _connectionString;

        public GetAllProductsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<Result<PagedResult<GetAllProductsResponse>>> Handle(GetPagedProductsQuery request)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = """
                SELECT COUNT(*) 
                FROM Product 
                WHERE IsDeleted = 0;

                SELECT Id, Name, UnitPrice, CreatedDate
                FROM Product
                WHERE IsDeleted = 0
                ORDER BY CreatedDate
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
                """;

            using var multi = await connection.QueryMultipleAsync(sql, new
            {
                Offset = (request.page - 1) * request.pageSize,
                PageSize = request.pageSize
            });

            int totalCount = await multi.ReadSingleAsync<int>();
            var products = (await multi.ReadAsync<Product>()).ToList();

            var responses = products
                .Select(p => new GetAllProductsResponse(p.Id, p.Name, p.UnitPrice))
                .ToList();

            PagedResult<GetAllProductsResponse> result = new PagedResult<GetAllProductsResponse>(responses, totalCount);
            return Result<PagedResult<GetAllProductsResponse>>.Success(result);
        }
    }
}
