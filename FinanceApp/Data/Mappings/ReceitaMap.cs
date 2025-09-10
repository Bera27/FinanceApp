using System.Globalization;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinanceApp.Data.Mappings
{
    public class ReceitaMap : IEntityTypeConfiguration<Receita>
    {
        public void Configure(EntityTypeBuilder<Receita> builder)
        {
            builder.ToTable("Receita");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Decricao)
                .HasColumnName("Descricao")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.Valor)
                .IsRequired()
                .HasColumnName("Valor");

            // Converte a data em string

            var dateConverter = new ValueConverter<DateTime, string>(
                    v => v.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    v => DateTime.ParseExact(v, "dd/MM/yyyy", CultureInfo.InvariantCulture));

            builder.Property(x => x.DataDeRecebimento)
                .IsRequired()
                .HasColumnName("DataRecebimento")
                .HasColumnType("TEXT")
                .HasConversion(dateConverter);

            builder
                .HasOne(x => x.Categoria)
                .WithMany(x => x.Receitas)
                .HasForeignKey(x => x.CategoriaId)
                .HasConstraintName("FK_Receita_Categoria")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}