using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Commands;
using BTM.Products.Application.Queries.GetProducts;
using BTM.Products.Contracts.ProductCommands;
using BTM.Products.Contracts.ProductDTOs;
using Microsoft.AspNetCore.Mvc;

namespace BTM.Products.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ProductsController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var results = await _dispatcher.Send<GetProductsQuery, IEnumerable<ProductDto>>(new GetProductsQuery());

            if (results == null || !results.Any())
                return NotFound();

            return Ok(results.ToList());
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
    }
}
