using CleanApiSample.Application.Abstractions;
using CleanApiSample.Infraestructure.Persistence;
using CleanApiSample.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration config)
        {
            var conn = config.GetConnectionString("DefaultConnection") ?? "Data Source=app.db";
            services.AddDbContext<AppDbContext>(option => option.UseSqlite(conn));
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
