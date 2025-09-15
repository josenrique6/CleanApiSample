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
    public record GetInvoicesByFiltersQuery(
        DateTime? fechaInicio,
        DateTime? fechaFin,
        string? ruc
        ): IRequest<List<InvoiceDto>>;

    public class GetInvoicesByFiltersQueryHandler : IRequestHandler<GetInvoicesByFiltersQuery, List<InvoiceDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public GetInvoicesByFiltersQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task<List<InvoiceDto>> Handle(GetInvoicesByFiltersQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepository.ListAsyncByFilters(request.fechaInicio, request.fechaFin, request.ruc, cancellationToken);
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
                    d.Amount,
                    new ProductDto(
                        d.Product?.Id ?? 0,
                        d.Product?.Code ?? string.Empty,
                        d.Product?.Name ?? string.Empty,
                        d.Product?.UnitPrice ?? 0,
                        d.Product?.Status ?? false)
                )).ToList()
            )).ToList();
        }
    }
}
