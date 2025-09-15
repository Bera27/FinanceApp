using FinanceApp.Data.Mappings;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FinanceApp.Data;

public class FinanceDataContext : DbContext
{
    private readonly string _databasePath;

    public FinanceDataContext(string databasePath)
    {
        _databasePath = databasePath;
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Despesa> Despesas { get; set; }
    public DbSet<MetaOrcamento> MetaOrcamentos { get; set; }
    public DbSet<Receita> Receitas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        Batteries_V2.Init();

        optionsBuilder.UseSqlite($"Data Source={_databasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoriaMap());
        modelBuilder.ApplyConfiguration(new DespesaMap());
        modelBuilder.ApplyConfiguration(new MetaOrcamentoMap());
        modelBuilder.ApplyConfiguration(new ReceitaMap());
    }
}

