using CleanApiSample.Application.Abstractions;
using CleanApiSample.Application.Dtos.Invoices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Features.Invoices.Queries
{
    public record GetInvoicesByIdQuery(int id) : IRequest<InvoiceDto?>;

    public class GetInvoicesByIdQueryHandler : IRequestHandler<GetInvoicesByIdQuery, InvoiceDto?>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoicesByIdQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceDto?> Handle(GetInvoicesByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetById(request.id, cancellationToken);
            if (invoice == null) return null;
            return new InvoiceDto(
                invoice.Id,
                invoice.Series,
                invoice.Number,
                invoice.Date,
                invoice.CustomerRuc,
                invoice.CustomerName,
                invoice.Currency,
                invoice.Subtotal,
                invoice.Tax,
                invoice.Total,
                invoice.Details.Select(d => new InvoiceDetailDto(
                    d.Id,
                    d.InvoiceId,
                    d.ProductId,
                    d.ProductCode,
                    d.ProductName,
                    d.Quantity,
                    d.Price,
                    d.Amount
                )).ToList()
            );
        }
    }
}
