using CleanApiSample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Abstractions
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> ListAsync(CancellationToken cancellationToken);
        Task<int> AddAsync(Invoice entity, CancellationToken cancellationToken);
        Task<Invoice?> GetById(int id, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
        Task<bool> Update(Invoice entity, CancellationToken cancellationToken);
    }
}
