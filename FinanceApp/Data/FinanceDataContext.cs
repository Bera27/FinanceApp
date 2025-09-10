using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data;

public class FinanceDataContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Despesa> Despesas { get; set; }
    public DbSet<MetaOrcamento> MetaOrcamentos { get; set; }
    public DbSet<Receita> Receitas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=finance.db");
    }
}

