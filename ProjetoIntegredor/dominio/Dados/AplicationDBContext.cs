using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegredor.model;

namespace ProjetoIntegredor.dominio.Dados
{
    // Classe responsavel pela conexão com o banco de dados e 
    //configuração de entidades e tabelas
    public class AplicationDBContext : DbContext
    {
    public AplicationDBContext(DbContextOptions<AplicationDBContext> options)
    : base (options)
    {
        
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuração da entidade Alocação
            modelBuilder.Entity<Alocacao>(entidade =>
            {
                entidade.ToTable("Alocacao","public");
                entidade.HasKey(x => x.IdAlocacao);
            });

            modelBuilder.Entity<Disciplina>(entidade =>
            {
              entidade.ToTable("Disciplina", "public");
              entidade.Ignore(e => e.Softwares);
              entidade.HasKey(x => x.IdDisciplina);
            });
            modelBuilder.Entity<Laboratorio>(entidade =>
            {
              entidade.ToTable("Laboratorio", "public");
              entidade.HasKey(x => x.IdLaboratorio);
            });
            modelBuilder.Entity<Software>(entidade =>
            {
                entidade.ToTable("Software", "public");
                entidade.HasKey(x => x.IdSoftware);
            });
            modelBuilder.Entity<Usuario>(entidade =>
            {
                entidade.ToTable("Usuario", "public");
                entidade.HasKey(x => x.IdUsuario);
            });
             
            
        }

    }
}