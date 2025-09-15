using System.Globalization;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinanceApp.Data.Mappings
{
    public class MetaOrcamentoMap : IEntityTypeConfiguration<MetaOrcamento>
    {
        public void Configure(EntityTypeBuilder<MetaOrcamento> builder)
        {
            builder.ToTable("MetaOrcamento");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ValorMaximo)
                .IsRequired()
                .HasColumnName("ValorMaximo");

            var dateConverter = new ValueConverter<DateTime, string>(
                    v => v.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    v => DateTime.ParseExact(v, "dd/MM/yyyy", CultureInfo.InvariantCulture));

            builder.Property(x => x.MesAno)
                .IsRequired()
                .HasColumnName("MesAno")
                .HasColumnType("TEXT")
                .HasConversion(dateConverter);

            builder
                .HasOne(x => x.Categoria)
                .WithMany(x => x.MetaOrcamentos)
                .HasForeignKey(x => x.CategoriaId)
                .HasConstraintName("FK_MetaOrcamento_Categoria")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}