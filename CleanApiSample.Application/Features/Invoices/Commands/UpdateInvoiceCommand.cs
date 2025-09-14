using CleanApiSample.Application.Abstractions;
using CleanApiSample.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Features.Invoices.Commands
{
    public record UpdateInvoiceCommand(
        int Id,
        string Series,
        string Number,
        DateTime Date,
        string CustomerRuc,
        string CustomerName,
        string Currency,
        decimal Subtotal,
        decimal Tax,
        decimal Total,
        List<UpdateInvoiceDetail> Details
    ) : IRequest<bool>;

    public record UpdateInvoiceDetail(
        int Id,
        int ProductId,
        string? ProductCode,
        string? ProductName,
        int Quantity,
        decimal Price,
        decimal Amount
    );

    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, bool>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public UpdateInvoiceCommandHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<bool> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = new Invoice
            {
                Id = request.Id,
                Series = request.Series,
                Number = request.Number,
                Date = request.Date,
                CustomerRuc = request.CustomerRuc,
                CustomerName = request.CustomerName,
                Currency = request.Currency,
                Subtotal = request.Subtotal,
                Tax = request.Tax,
                Total = request.Total,
                Details = request.Details.Select(d => new InvoiceDetail
                {
                    Id = d.Id,
                    InvoiceId = request.Id,
                    ProductId = d.ProductId,
                    ProductCode = d.ProductCode,
                    ProductName = d.ProductName,
                    Quantity = d.Quantity,
                    Price = d.Price,
                    Amount = d.Amount
                }).ToList()
            };
            return await _invoiceRepository.Update(invoice, cancellationToken);
        }
    }
}
