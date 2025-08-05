using BTM.Products.Api.Factories.Abstractions;
using BTM.Products.ApiClient.Out;
using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Commands;
using BTM.Products.Application.Queries.GetProducts;
using BTM.Products.Application.Results;
using BTM.Products.Contracts.ProductCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BTM.Products.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly IProductFactory _productFactory;

        public ProductsController(IDispatcher dispatcher, IProductFactory productFactory)
        {
            _dispatcher = dispatcher;
            _productFactory = productFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            GetProductsQuery query = new GetProductsQuery(id);

            Result<List<GetProductResponse>> results = await _dispatcher.Send<GetProductsQuery, Result<List<GetProductResponse>>>(query, cancellationToken: CancellationToken.None);

            if (results == null || !results.IsSuccess)
            {
                return NotFound(results?.ErrorMessage);
            }

            IEnumerable<ProductResponse> response = _productFactory.Create(results.Data);

            return Ok(response);
        }

        // POST  
        [HttpPost]
        public IActionResult Create([FromBody] CreateProductRequest product)
        {
            if (product == null)
                return BadRequest();

            AddProductCommand command = new AddProductCommand(product.Name, product.Price);
            _dispatcher.Send(command);

            return CreatedAtAction(nameof(Index), product);
        }

        [HttpPost("request-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestToken()
        {
            // IdentityServer token endpoint
            var tokenEndpoint = "https://localhost:5001/connect/token";
            var clientId = "swagger";
            var clientSecret = "secret";
            var scope = "ProductsAPI.fullaccess";

            using var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
            request.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("scope", scope)
            });

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, content);
            }

            // Optionally, parse and return just the access_token
            var json = JsonDocument.Parse(content);
            if (json.RootElement.TryGetProperty("access_token", out var token))
            {
                return Ok(new { access_token = token.GetString() });
            }

            return BadRequest("Token not found in response.");
        }
    }
}
