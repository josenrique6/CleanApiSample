using CleanApiSample.Application.Dtos.Invoices;
using CleanApiSample.Application.Features.Invoices;
using CleanApiSample.Application.Features.Invoices.Commands;
using CleanApiSample.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanApiSample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<InvoiceDto>> Get()
        {
            return _mediator.Send(new GetInvoicesQuery());
        }

        [HttpPost]
        public Task<int> Add([FromBody] AddInvoiceCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpGet("id/{id}")]
        public Task<InvoiceDto?> GetById(int id)
        {
            return _mediator.Send(new GetInvoicesByIdQuery(id));
        }

        [HttpDelete]
        public Task<bool> Delete([FromBody] DeleteInvoiceCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpPut]
        public Task<bool> Update([FromBody] UpdateInvoiceCommand command)
        {
            return _mediator.Send(command);
        }
    }
}
