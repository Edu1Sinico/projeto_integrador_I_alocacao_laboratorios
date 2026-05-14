using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Infrastructure.Mappings
{
    public class LaboratorioMap : IEntityTypeConfiguration<Laboratorio>
    {
        public void Configure(
            EntityTypeBuilder<Laboratorio> builder)
        {
            builder.ToTable("Laboratorios");

            builder.HasKey(x => x.IdLaboratorio);

            builder.Property(x => x.NumeroLaboratorio)
                .IsRequired();

            builder.Property(x => x.QuantidadeComputadores)
                .IsRequired();

            builder.Property(x => x.CapacidadeMaximaAlunos)
                .IsRequired();

            builder.HasMany(x => x.Alocacoes)
                .WithOne(x => x.Laboratorio)
                .HasForeignKey(x => x.LaboratorioId);
        }
    }
}