using CleanApiSample.Application.Abstractions;
using CleanApiSample.Application.Dtos.Invoices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Features.Invoices
{
    public record GetInvoicesQuery() : IRequest<List<InvoiceDto>>;

    public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, List<InvoiceDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<List<InvoiceDto>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepository.ListAsync(cancellationToken);
            return invoices.Select(i => new InvoiceDto(
                i.Id,
                i.Series,
                i.Number,
                i.Date,
                i.CustomerRuc,
                i.CustomerName,
                i.Currency,
                i.Subtotal,
                i.Tax,
                i.Total,
                i.Details.Select(d => new InvoiceDetailDto(
                    d.Id,
                    d.InvoiceId,
                    d.ProductId,
                    d.ProductCode,
                    d.ProductName,
                    d.Quantity,
                    d.Price,
                    d.Amount
                )).ToList()
            )).ToList();
        }
    }
}
