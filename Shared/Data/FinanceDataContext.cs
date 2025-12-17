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


        // Categorias da Depesas
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Casa", Tipo = CategoriaTipo.Despesa, ImagemPath = "casa.png" },
            new Categoria { Id = 2, Nome = "Carro", Tipo = CategoriaTipo.Despesa, ImagemPath = "furgao.png" },
            new Categoria { Id = 3, Nome = "Viagem", Tipo = CategoriaTipo.Despesa, ImagemPath = "viagem.png" },
            new Categoria { Id = 4, Nome = "Conta", Tipo = CategoriaTipo.Despesa, ImagemPath = "taxaatraso.png" },
            new Categoria { Id = 5, Nome = "Alimentação", Tipo = CategoriaTipo.Despesa, ImagemPath = "talheres.png" },
            new Categoria { Id = 6, Nome = "Educação", Tipo = CategoriaTipo.Despesa, ImagemPath = "educacao.png" },
            new Categoria { Id = 7, Nome = "Saúde", Tipo = CategoriaTipo.Despesa, ImagemPath = "primeirossocorros.png" },
            new Categoria { Id = 8, Nome = "Outros", Tipo = CategoriaTipo.Despesa, ImagemPath = "process.png" },

            //Categoria de Receitas
            new Categoria { Id = 9, Nome = "Salário", Tipo = CategoriaTipo.Receita, ImagemPath = "salario.png"},
            new Categoria { Id = 10, Nome = "Poupança", Tipo = CategoriaTipo.Receita, ImagemPath = "poupanca.png" },
            new Categoria { Id = 11, Nome = "Investimento", Tipo = CategoriaTipo.Receita, ImagemPath = "investimento.png" },
            new Categoria { Id = 12, Nome = "Outros", Tipo = CategoriaTipo.Receita, ImagemPath = "process.png"});
    }
}

