using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Dtos.Invoices
{
    public record InvoiceDetailDto(
        int Id,
        int InvoiceId,
        int ProductId,
        string? ProductCode,
        string? ProductName,
        int Quantity,
        decimal Price,
        decimal Amount
    );
}
