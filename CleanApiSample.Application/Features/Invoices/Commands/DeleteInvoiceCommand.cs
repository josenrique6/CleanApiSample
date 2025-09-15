using CleanApiSample.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Features.Invoices.Commands
{
    public record DeleteInvoiceCommand(int id) : IRequest<bool>;

    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, bool>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public DeleteInvoiceCommandHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task<bool> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            await _invoiceRepository.Delete(request.id, cancellationToken);
            return true;
        }
    }
}
