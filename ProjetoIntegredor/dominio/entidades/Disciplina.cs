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

        public string NomeDisciplina
        {
            get => nomeDisciplina;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("O nome da disciplina não pode ser vazia!");
                nomeDisciplina = value;
            }
        }

        public int QtdeAlunos
        {
            get => qtdeAlunos;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Quantidade de alunos não pode ser zero ou negativo!");
                qtdeAlunos = value;
            }
        }

        public Disciplina(string nomeDisciplina, int qtde_alunos)
        {
            IdDisciplina = contador++;
            NomeDisciplina = nomeDisciplina;
            QtdeAlunos = qtde_alunos;
        }
    }
}