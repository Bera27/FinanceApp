using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceApp.Data;
using Microsoft.EntityFrameworkCore.Design;

namespace FinanceApp.Shared.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<FinanceDataContext>
    {
        public FinanceDataContext CreateDbContext(string[] args)
        {
            var DbPath = Path.Combine(Directory.GetCurrentDirectory(), "migrator.db3");
            return new FinanceDataContext(DbPath);
        }
    }
}
