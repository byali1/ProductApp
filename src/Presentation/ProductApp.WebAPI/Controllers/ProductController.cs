using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Application.Features.Commands.AddProductRange;
using ProductApp.Application.Features.Commands.CreateProduct;
using ProductApp.Application.Features.Commands.UpdateProduct;
using ProductApp.Application.Features.Queries.GetAllProducts;
using ProductApp.Application.Features.Queries.GetProductById;
using ProductApp.Application.Interfaces.Repository;

namespace ProductApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var query = new GetProductByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddRangeProducts([FromBody] AddProductRangeCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"An error occurred: {e.Message}");
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductViewModel product, [FromRoute] Guid id)
        {
            var command = new UpdateProductCommand { Id = id, UpdateProduct = product };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> UpdateProductQuantity([FromBody] int quantity, [FromRoute] Guid id)
        {
            try
            {
                var command = new UpdateProductQuantityCommand { Id = id, Quantity = quantity };
                var result = await _mediator.Send(command);

                if (!result.IsSuccess)
                {
                    return BadRequest(result.Message);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }
}
