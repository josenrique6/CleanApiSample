using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Infraestructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var option = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=app.db")
                .Options;
            return new AppDbContext(option);
        }
    }
}
