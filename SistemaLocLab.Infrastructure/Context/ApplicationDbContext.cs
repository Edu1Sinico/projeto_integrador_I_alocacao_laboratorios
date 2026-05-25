using Microsoft.EntityFrameworkCore;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<Laboratorios> Laboratorios { get; set; }

        public DbSet<Software> Softwares { get; set; }

        public DbSet<Disciplina> Disciplinas { get; set; }

        public DbSet<Alocacao> Alocacoes { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Alocacao>(entidade =>
            {
                entidade.ToTable("Alocacao");

                entidade.HasKey(x => x.IdAlocacao);

                entidade.HasOne(x => x.Laboratorio)
                    .WithMany(x => x.Alocacoes)
                    .HasForeignKey(x => x.LaboratorioId);

                entidade.HasOne(x => x.Disciplina)
                    .WithMany(x => x.Alocacoes)
                    .HasForeignKey(x => x.DisciplinaId);

                entidade.HasOne(x => x.Usuario)
                    .WithMany()
                    .HasForeignKey(x => x.UsuarioId);
            });

            modelBuilder.Entity<Disciplina>(entidade =>
            {
                entidade.ToTable("Disciplina");
                entidade.HasKey(x => x.IdDisciplina);
            });

            modelBuilder.Entity<Laboratorios>(entidade =>
            {
                entidade.ToTable("Laboratorio");
                entidade.HasKey(x => x.IDLaboratorio);
            });

            modelBuilder.Entity<Software>(entidade =>
            {
                entidade.ToTable("Software");

                entidade.HasKey(x => x.IdSoftware);

                entidade.HasMany(x => x.Disciplinas)
                    .WithMany(x => x.Softwares);

                entidade.HasMany(x => x.Laboratorios)
                    .WithMany(x => x.Softwares);
            });

            modelBuilder.Entity<Usuarios>(entidade =>
            {
                entidade.ToTable("Usuario");
                entidade.HasKey(x => x.ID);
            });
        }
    }
}