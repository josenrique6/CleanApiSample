using CleanApiSample.Application.Abstractions;
using CleanApiSample.Domain.Entities;
using CleanApiSample.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Product entity, CancellationToken cancellationToken)
        {
            _db.Products.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
            if (product == null) return false;
            _db.Products.Remove(product);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Product?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _db.Products.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public Task<List<Product>> ListAsync(CancellationToken cancellationToken)
        {
            return _db.Products.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<bool> Update(Product entity, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FirstOrDefaultAsync(w => w.Id == entity.Id, cancellationToken);
            if (product == null) throw new Exception("Producto no encontrado");
            product.Code = entity.Code;
            product.Name = entity.Name;
            product.UnitPrice = entity.UnitPrice;
            product.Status = entity.Status;
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
