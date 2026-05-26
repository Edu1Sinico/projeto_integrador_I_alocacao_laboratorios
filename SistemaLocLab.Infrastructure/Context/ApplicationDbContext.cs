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

        // TABELAS (DbSets)

        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<Laboratorios> Laboratorios { get; set; }

        public DbSet<Software> Softwares { get; set; }

        public DbSet<Disciplina> Disciplinas { get; set; }

        public DbSet<Alocacao> Alocacoes { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Procura e aplica automaticamente
            // todos os IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }
    }
}