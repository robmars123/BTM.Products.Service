using BTM.Products.Application.Abstractions;
using BTM.Products.Application.Abstractions.Repositories;
using BTM.Products.Application.Results;
using Microsoft.Extensions.Configuration;

namespace BTM.Products.Application.Queries.GetProducts
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
    {
        private readonly string _connectionString;
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IConfiguration configuration, IProductRepository productRepository)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _productRepository = productRepository;
        }
        public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery request)
        {
            //todo add Cache
            var response = await _productRepository.GetByIdAsync(request.Id);
            GetProductByIdResponse getProductByIdResponse = new GetProductByIdResponse(response.Id, response.Name, response.UnitPrice);
            return Result<GetProductByIdResponse>.Success(getProductByIdResponse);
        }
    }
}
