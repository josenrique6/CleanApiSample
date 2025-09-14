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
        }
    }
}
