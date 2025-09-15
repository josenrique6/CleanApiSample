using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Dtos.Invoices
{
    public record InvoiceDto(
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
        List<InvoiceDetailDto> Details
    );
}
