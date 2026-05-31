using System;
using System.Collections.Generic;
using System.Linq;
namespace SistemaLocLab.Domain.Entities
{
    public class Disciplina
    {
        //Encapsulamento
        public Guid IdDisciplina { get; private set; }
        public string NomeDisciplina { get; private set; } = string.Empty;
        public int QtdeAlunos { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        // RELACIONAMENTOS
        public List<Software> Softwares
        { get; private set; } = new();
        public List<Alocacao> Alocacoes
        { get; private set; } = new();
        protected Disciplina() { }
        public Disciplina(string nomeDisciplina, int qtdeAlunos)
        {
            Validacao(nomeDisciplina, qtdeAlunos);
            IdDisciplina = Guid.NewGuid();
            NomeDisciplina = nomeDisciplina.Trim();
            QtdeAlunos = qtdeAlunos;
            DataCriacao = DateTime.UtcNow;
        }

        public void Atualizar(string nomeDisciplina, int qtdeAlunos)
        {
            Validacao(nomeDisciplina, qtdeAlunos);
            NomeDisciplina = nomeDisciplina.Trim();
            QtdeAlunos = qtdeAlunos;
            DataAtualizacao = DateTime.UtcNow;
        }
        public void VincularSoftware(Software software)
        {
            if (software == null)
                throw new ArgumentException(
                "Software inválido.");
            bool jaExiste = Softwares.Any(x => x.IdSoftware == software.IdSoftware);
            if (jaExiste)
                throw new Exception("Software já vinculado à disciplina.");
            Softwares.Add(software);
        }
        private void Validacao(string nomeDisciplina, int qtdeAlunos)
        {
            ValidacaoNome(nomeDisciplina);
            ValidacaoQuantidadeAlunos(qtdeAlunos);
        }
        private void ValidacaoNome(string nomeDisciplina)
        {
            if (string.IsNullOrWhiteSpace(nomeDisciplina))
                throw new ArgumentException(
                "Nome da disciplina obrigatório.");
            
        if (nomeDisciplina.Length < 2)
                throw new ArgumentException(
                "Nome da disciplina inválido.");
            if (nomeDisciplina.Length > 100)
                throw new ArgumentException(
                "Nome da disciplina muito grande.");
        }
        private void ValidacaoQuantidadeAlunos(int qtdeAlunos)
        {
            if (qtdeAlunos <= 0)
                throw new ArgumentException(
                "Quantidade de alunos inválida.");
            if (qtdeAlunos > 100)
                throw new ArgumentException(
                "Quantidade de alunos excedida.");
        }
    }
}
