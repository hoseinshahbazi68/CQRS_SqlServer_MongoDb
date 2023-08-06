using CQRS.Service.ProductApplication.Commands.AddProduct.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Core.ProductApplication.Commands.DeleteProduct;
using Sample.Core.ProductApplication.Commands.UpdateProduct;
using Sample.Core.ProductApplication.Queries.GetProductAll;
using Sample.Core.ProductApplication.Queries.GetProductByName;

namespace CQRS.Api.Controllers
{
    [ApiController]
    [Route("Product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddProductCommand model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = await _mediator.Send(model, cancellationToken);

            return Ok(command);
        }

        [HttpGet("GetProductByName")]
        public async Task<IActionResult> GetProductByName([FromQuery] GetProductByNameQuery model, CancellationToken cancellationToken)
        {
            var query = await _mediator.Send(model, cancellationToken);

            return Ok(query);
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct([FromQuery] GetProductAll model, CancellationToken cancellationToken)
        {
            var query = await _mediator.Send(model, cancellationToken);

            return Ok(query);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand model, CancellationToken cancellationToken)
        {
            var query = await _mediator.Send(model, cancellationToken);

            return Ok(query);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommand model, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(model, cancellationToken);

            if (result)
                return Ok();

            return BadRequest();
        }


    }
}
