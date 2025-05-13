using BTM.Products.Application.Abstractions.Mediator;
using BTM.Products.Application.Commands;
using BTM.Products.Application.Queries.GetProducts;
using BTM.Products.Contracts.ProductCommands;
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
        public IActionResult Index()
        {
            var results = _dispatcher.Send(new GetProductsQuery());

            if (results == null || !results.Result.Any())
                return NotFound();

            return Ok(results.Result.ToList());
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
