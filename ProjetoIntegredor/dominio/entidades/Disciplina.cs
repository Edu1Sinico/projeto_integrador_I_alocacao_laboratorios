using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegredor.model
{
    public class Disciplina
    {
        private static int contador = 1;
        public int IdDisciplina { get; private set; }
        private string nomeDisciplina;
        private int qtdeAlunos;
        public List<Software> Softwares { get; private set; } = new();

        public string NomeDisciplina
        {
            get => nomeDisciplina;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Nome da disciplina inválida!");
                nomeDisciplina = value;
            }
        }

        public int QtdeAlunos
        {
            get => qtdeAlunos;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Quantidade de alunos inválido!");
                qtdeAlunos = value;
            }
        }

        public Disciplina(string nomeDisciplina, int qtdeAlunos)
        {
            IdDisciplina = contador++;
            NomeDisciplina = nomeDisciplina;
            QtdeAlunos = qtdeAlunos;
        }

        // Adicionar Software para Disciplina
        public void AdicionarSoftware(Software software)
        {
            if (software == null)
                throw new ArgumentNullException(nameof(software)); // trata o erro caso a referência do parâmetro for null

            if (!Softwares.Contains(software)) // Só adiciona se a disciplina não estiver com nenhum software vinculado a ela
                Softwares.Add(software);

        }

        // Remover Software da Disciplina
        public void RemoverSoftware(Software software)
        {
            Softwares.Remove(software);
        }
    }
}