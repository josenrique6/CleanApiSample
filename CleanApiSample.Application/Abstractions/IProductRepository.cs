using CleanApiSample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Abstractions
{
    public interface IProductRepository
    {
        Task<List<Product>> ListAsync(CancellationToken cancellationToken);
        Task<int> AddAsync (Product entity, CancellationToken cancellationToken);
        Task<Product?> GetById(int id, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
        Task<bool> Update(Product entity, CancellationToken cancellationToken);
    }
}
