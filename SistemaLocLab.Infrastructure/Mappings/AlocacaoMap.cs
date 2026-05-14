using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Infrastructure.Mappings
{
    public class AlocacaoMap : IEntityTypeConfiguration<Alocacao>
    {
        public void Configure(
            EntityTypeBuilder<Alocacao> builder)
        {
            builder.ToTable("Alocacoes");

            builder.HasKey(x => x.IdAlocacao);

            builder.Property(x => x.Data)
                .IsRequired();

            builder.Property(x => x.HoraInicio)
                .IsRequired();

            builder.Property(x => x.HoraFim)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Alocacoes)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasOne(x => x.Laboratorio)
                .WithMany(x => x.Alocacoes)
                .HasForeignKey(x => x.LaboratorioId);

            builder.HasOne(x => x.Disciplina)
                .WithMany(x => x.Alocacoes)
                .HasForeignKey(x => x.DisciplinaId);
        }
    }
}