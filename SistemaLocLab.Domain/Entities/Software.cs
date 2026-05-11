using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace SistemaLocLab.Domain.Entities
{
    public class Software
    {
        public Guid IdSoftware { get; private set; }

        public string NomeSoftware { get; private set; }

        public string Versao { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public DateTime? DataAtualizacao { get; private set; }

        // RELACIONAMENTOS

        public List<Laboratorios> Laboratorios
        { get; private set; } = new();

        public List<Disciplina> Disciplinas
        { get; private set; } = new();

        protected Software() { }

        public Software(
            string nomeSoftware,
            string versao)
        {
            Validacao(nomeSoftware, versao);

            IdSoftware = Guid.NewGuid();

            NomeSoftware = nomeSoftware.Trim();

            Versao = versao.Trim();

            DataCriacao = DateTime.Now;
        }

        public void Atualizar(
            string nomeSoftware,
            string versao)
        {
            Validacao(nomeSoftware, versao);

            NomeSoftware = nomeSoftware.Trim();

            Versao = versao.Trim();

            DataAtualizacao = DateTime.Now;
        }

        //Vincula um software a um laboratorio
        public void VincularLaboratorio(
            Laboratorios laboratorio)
        {

            if (laboratorio == null)
                throw new ArgumentException(
                    "Laboratório inválido.");

            //verifica se existe algum laboratorio com o mesmo ID dentro da lista, se existir da erro
            bool jaExiste =
                Laboratorios.Any(
                    x => x.IDLaboratorio ==
                    laboratorio.IDLaboratorio);

            //se ja existir retorna mensagem
            if (jaExiste)
                throw new Exception(
                    "Software já vinculado ao laboratório.");

            Laboratorios.Add(laboratorio);
        }
        //Mesma logica do metodo VincularLaboratorio (adiciona um software a uma disciplina)
        public void VincularDisciplina(
    Disciplina disciplina)
        {
            if (disciplina == null)
                throw new ArgumentException(
                    "Disciplina inválida.");

            bool jaExiste =
                Disciplinas.Any(
                    x => x.IdDisciplina ==
                    disciplina.IdDisciplina);

            if (jaExiste)
                throw new Exception(
                    "Software já vinculado à disciplina.");

            Disciplinas.Add(disciplina);
        }

        private void Validacao(
            string nomeSoftware,
            string versao)
        {
            ValidacaoNome(nomeSoftware);

            ValidacaoVersao(versao);
        }

        private void ValidacaoNome(
            string nomeSoftware)
        {
            if (string.IsNullOrWhiteSpace(nomeSoftware))
                throw new ArgumentException(
                    "Nome do software obrigatório.");

            if (nomeSoftware.Length < 2)
                throw new ArgumentException(
                    "Nome do software inválido.");

            if (nomeSoftware.Length > 100)
                throw new ArgumentException(
                    "Nome do software muito grande.");
        }

        private void ValidacaoVersao(
            string versao)
        {
            if (string.IsNullOrWhiteSpace(versao))
                throw new ArgumentException(
                    "Versão obrigatória.");
        }
    }
}
