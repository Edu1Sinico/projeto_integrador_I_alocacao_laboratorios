using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Infrastructure.Mappings
{
    public class LaboratorioMap :
        IEntityTypeConfiguration<Laboratorios>
    {
        public void Configure(
            EntityTypeBuilder<Laboratorios> builder)
        {
            builder.ToTable("Laboratorios");

            builder.HasKey(x => x.IDLaboratorio);

            builder.Property(x => x.NumeroLaboratorio)
                .IsRequired();

            builder.Property(x => x.qtdeComputador)
                .IsRequired();

            builder.Property(x => x.capacidadeMaxAluno)
                .IsRequired();

            builder.HasMany(x => x.Alocacoes)
                .WithOne(x => x.Laboratorio)
                .HasForeignKey(x => x.LaboratorioId);
        }
    }
}