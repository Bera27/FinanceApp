using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinanceApp.Data.Mappings
{
    public class DespesaMap : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.ToTable("Despesa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Descricao)
                .IsRequired(false)
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

            builder.Property(x => x.DataVencimento)
                .IsRequired()
                .HasColumnName("DataVencimento")
                .HasColumnType("TEXT")
                .HasConversion(dateConverter);

            builder.Property(x => x.DataPagamento)
                .HasColumnName("DataPagamento")
                .HasColumnType("TEXT")
                .HasConversion(dateConverter);

            builder
                .HasOne(x => x.Categoria)
                .WithMany(x => x.Despesas)
                .HasForeignKey(x => x.CategoriaId)
                .HasConstraintName("FK_Despesa_Categoria")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}