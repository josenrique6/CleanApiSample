using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Dtos.Products
{
    public record ProductDto(
        int Id,
        string Code,
        string Name,
        decimal UnitPrice,
        bool Status
    );
}
