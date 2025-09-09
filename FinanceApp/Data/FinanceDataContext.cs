using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data
{
    public class FinanceDataContext : DbContext
    {
        public FinanceDataContext(DbContextOptions<FinanceDataContext> options) : base(options) { }
    }
}
