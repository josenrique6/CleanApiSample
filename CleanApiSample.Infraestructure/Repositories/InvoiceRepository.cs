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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _db;
        public InvoiceRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Invoice entity, CancellationToken cancellationToken)
        {
            _db.Invoices.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var invoice = await _db.Invoices.Include(i => i.Details)
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
            if (invoice == null) return false;
            _db.Invoices.Remove(invoice);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Invoice?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _db.Invoices.AsNoTracking()
                .Include(i => i.Details)
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        }

        public Task<List<Invoice>> ListAsync(CancellationToken cancellationToken)
        {
            return _db.Invoices.AsNoTracking()
                .Include(i => i.Details)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> Update(Invoice entity, CancellationToken cancellationToken)
        {
            var invoice = await _db.Invoices.Include(i => i.Details)
                .FirstOrDefaultAsync(w => w.Id == entity.Id, cancellationToken);
            if (invoice == null) throw new Exception("Invoice not found");
            invoice.Series = entity.Series;
            invoice.Number = entity.Number;
            invoice.Date = entity.Date;
            invoice.CustomerRuc = entity.CustomerRuc;
            invoice.CustomerName = entity.CustomerName;
            invoice.Currency = entity.Currency;
            invoice.Subtotal = entity.Subtotal;
            invoice.Tax = entity.Tax;
            invoice.Total = entity.Total;
            _db.InvoiceDetails.RemoveRange(invoice.Details);
            invoice.Details = entity.Details;
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
