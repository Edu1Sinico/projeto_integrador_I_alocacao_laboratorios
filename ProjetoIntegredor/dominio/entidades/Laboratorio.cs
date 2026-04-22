using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegredor.model
{
    public class Laboratorio
    {
        private static int contador = 1;
        public int Id_laboratorio { get; private set; }
        private int num_laboratorio;
        private int qtde_computador;
        private int capacidade_max_aluno;

        public int Num_laboratorio
        {
            get { return num_laboratorio; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("O número do laboratório não pode ser negativo!");
                num_laboratorio = value;
            }
        }

        public int Qtde_computador
        {
            get { return Qtde_computador; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("O número da quantidade de computadores não pode ser negativo!");
                qtde_computador = value;
            }
        }

        public int Capacidade_max_aluno
        {
            get { return capacidade_max_aluno; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("A capacidade máxima de alunos não pode ser negativo!");
                capacidade_max_aluno = value;
            }
        }
    }
}