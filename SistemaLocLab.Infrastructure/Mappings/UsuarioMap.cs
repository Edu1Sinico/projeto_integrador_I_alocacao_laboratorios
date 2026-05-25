using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Infrastructure.Mappings
{
    public class UsuarioMap :
        IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(
            EntityTypeBuilder<Usuarios> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.RE)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.SenhaHash)
                .IsRequired();

            builder.Property(x => x.Tipo)
                .IsRequired();

            builder.HasMany(x => x.Alocacoes)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.UsuarioId);
        }
    }
}