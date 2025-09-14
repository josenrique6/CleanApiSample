using CleanApiSample.Application.Dtos.Products;
using CleanApiSample.Application.Features.Products;
using CleanApiSample.Application.Features.Products.Commands;
using CleanApiSample.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanApiSample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<ProductDto>> Get()
        {
            return _mediator.Send(new GetProductsQuery());
        }

        [HttpPost]
        public Task<int> Add([FromBody] AddProductCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpGet("id/{_id}")]
        public Task<ProductDto?> GetById(int _id)
        {
            return _mediator.Send(new GetProductsByIdQuery(_id));
        }

        [HttpDelete]
        public Task<bool> Delete([FromBody] DeleteProductCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpPut]
        public Task<bool> Update([FromBody] UpdateProductoCommand command)
        {
            return _mediator.Send(command);
        }
    }
}
