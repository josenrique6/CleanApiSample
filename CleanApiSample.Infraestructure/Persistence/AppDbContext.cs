using CleanApiSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Infraestructure.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(b =>
            {
                b.ToTable("Products");
                b.HasKey(p => p.Id);
                b.Property(p => p.Code).IsRequired().HasMaxLength(50);
                b.Property(p => p.Name).IsRequired().HasMaxLength(200);
                b.Property(p => p.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Invoice>(b =>
            {
                b.ToTable("Invoices");
                b.HasKey(i => i.Id);
                b.Property(i => i.Series).IsRequired().HasMaxLength(10);
                b.Property(i => i.Number).IsRequired().HasMaxLength(20);
                b.Property(i => i.Date).IsRequired();
                b.Property(i => i.CustomerRuc).IsRequired().HasMaxLength(20);
                b.Property(i => i.CustomerName).IsRequired().HasMaxLength(200);
                b.Property(i => i.Currency).IsRequired().HasMaxLength(3);
                b.Property(i => i.Subtotal).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(i => i.Tax).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(i => i.Total).IsRequired().HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<InvoiceDetail>(b =>
            {
                b.ToTable("InvoiceDetails");
                b.HasKey(d => d.Id);
                b.Property(d => d.ProductCode).HasMaxLength(50);
                b.Property(d => d.ProductName).HasMaxLength(200);
                b.Property(d => d.Price).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(d => d.Amount).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(d => d.Quantity).IsRequired();
                b.HasOne(d => d.Invoice)
                    .WithMany(i => i.Details)
                    .HasForeignKey(d => d.InvoiceId);
            });
        }
    }
}
