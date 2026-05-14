using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Infrastructure.Mappings
{
    public class SoftwareMap : IEntityTypeConfiguration<Software>
    {
        public void Configure(
            EntityTypeBuilder<Software> builder)
        {
            builder.ToTable("Softwares");

            builder.HasKey(x => x.IdSoftware);

            builder.Property(x => x.NomeSoftware)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Versao)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}