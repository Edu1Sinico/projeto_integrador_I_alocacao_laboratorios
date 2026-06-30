using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Infrastructure.Mappings
{
    public class DisciplinaMap : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(
            EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable("Disciplinas");

            builder.HasKey(x => x.IdDisciplina);

            builder.Property(x => x.NomeDisciplina)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.QtdeAlunos)
                .IsRequired();

            builder.HasMany(x => x.Alocacoes)
                .WithOne(x => x.Disciplina)
                .HasForeignKey(x => x.DisciplinaId);
        }
    }
}