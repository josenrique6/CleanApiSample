using CleanApiSample.Application.Abstractions;
using CleanApiSample.Application.Dtos.Invoices;
using CleanApiSample.Application.Dtos.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Features.Invoices.Queries
{
    public record GetInvoiceByIdQuery(int id) : IRequest<InvoiceDto?>;

    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto?>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public GetInvoiceByIdQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task<InvoiceDto?> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var i = await _invoiceRepository.GetById(request.id, cancellationToken);
            if (i == null) return null;
            return new InvoiceDto(
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
                    d.Amount,
                    new ProductDto(
                        d.Product?.Id ?? 0,
                        d.Product?.Code ?? string.Empty,
                        d.Product?.Name ?? string.Empty,
                        d.Product?.UnitPrice ?? 0,
                        d.Product?.Status ?? false)
                )).ToList()
            );
        }
    }
}
