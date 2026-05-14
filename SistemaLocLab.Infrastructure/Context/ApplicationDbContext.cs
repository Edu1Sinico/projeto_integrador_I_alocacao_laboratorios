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

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Laboratorio> Laboratorios { get; set; }

        public DbSet<Software> Softwares { get; set; }

        public DbSet<Disciplina> Disciplinas { get; set; }

        public DbSet<Alocacao> Alocacoes { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }
        
    }
}