using CleanApiSample.Application.Dtos.Invoices;
using CleanApiSample.Application.Dtos.Products;
using CleanApiSample.Application.Features.Invoices.Commands;
using CleanApiSample.Application.Features.Invoices.Queries;
using CleanApiSample.Application.Features.Products;
using CleanApiSample.Application.Features.Products.Commands;
using CleanApiSample.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanApiSample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task<int> Add([FromBody] AddInvoiceCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpGet]
        public Task<List<InvoiceDto>> Get()
        {
            return _mediator.Send(new GetInvoicesQuery());
        }

        [HttpGet("id/{_id}")]
        public Task<InvoiceDto?> GetById(int _id)
        {
            return _mediator.Send(new GetInvoiceByIdQuery(_id));
        }

        [HttpPut]
        public Task<bool> Update([FromBody] UpdateInvoiceCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpDelete]
        public Task<bool> Delete([FromBody] DeleteInvoiceCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpPost("Filtros")]
        public Task<List<InvoiceDto>> GetByFilters([FromBody] GetInvoicesByFiltersQuery command)
        {
            return _mediator.Send(command);
        }
    }
}
