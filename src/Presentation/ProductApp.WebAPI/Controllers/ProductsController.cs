using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Application.Features.Commands.AddProductRange;
using ProductApp.Application.Features.Commands.CreateProduct;
using ProductApp.Application.Features.Commands.UpdateProduct;
using ProductApp.Application.Features.Commands.UpdateProductRange;
using ProductApp.Application.Features.Queries.GetAllProducts;
using ProductApp.Application.Features.Queries.GetProductById;

namespace ProductApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);

            _logger.LogInformation($"GetAllProducts() is called. Request Id:{result.RequestId}");

            return Ok(result);

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var query = new GetProductByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            _logger.LogInformation($"GetProductById() is called. Request Id:{result.RequestId}");

            if (result.IsSuccess)
                return Ok(result);

            _logger.LogInformation($"GetProductById() is called BUT, NOT FOUND DATA! Request Id:{result.RequestId}");
            return NotFound(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddRangeProducts([FromBody] AddProductRangeCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductViewModel product, [FromRoute] Guid id)
        {
            var command = new UpdateProductCommand { Id = id, UpdateProduct = product };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRangeProduct([FromBody] List<UpdateProductRequest> products)
        {
            var command = new UpdateProductRangeCommand
            {
                Products = products.Select(product => new UpdateProductRangeViewModel
                {
                    Id = product.Id,
                    Name = product.Product.Name,
                    Price = product.Product.Price,
                    Quantity = product.Product.Quantity
                }).ToList()
            };

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }




        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> UpdateProductQuantity([FromBody] int quantity, [FromRoute] Guid id)
        {
            var command = new UpdateProductQuantityCommand { Id = id, Quantity = quantity };
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);

        }

    }
}
