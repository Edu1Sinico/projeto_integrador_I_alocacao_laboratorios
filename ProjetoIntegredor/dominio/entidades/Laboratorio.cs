using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;

namespace ProjetoIntegredor.model
{
    public class Laboratorio
    {
        private static int contador = 1;
        public int IdLaboratorio { get; private set; }
        private int numLaboratorio;
        private int qtdeComputador;
        private int capacidadeMaxAluno;
        public char Blocos { get; set; }
        public Disponibilidade StatusDisponibilidade { get; set; } = Disponibilidade.D; // Define automaticamente a disponibilidade como "D - Disponível".
        public int NumLaboratorio
        {
            get => numLaboratorio;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("O número do laboratório não pode ser zero ou negativo!");
                numLaboratorio = value;
            }
        }

        public int QtdeComputador
        {
            get => qtdeComputador;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("O número da quantidade de computadores não pode ser zero ou negativo!");
                qtdeComputador = value;
            }
        }

        public int CapacidadeMaxAluno
        {
            get => capacidadeMaxAluno;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("A capacidade máxima de alunos não pode ser zero ou negativo!");
                capacidadeMaxAluno = value;
            }
        }

        public Laboratorio(int numLaboratorio, int qtdeComputador, int capacidadeMaxAluno, char blocos)
        {
            IdLaboratorio = contador++;
            NumLaboratorio = numLaboratorio;
            QtdeComputador = qtdeComputador;
            CapacidadeMaxAluno = capacidadeMaxAluno;
            Blocos = blocos;
        }
    }
}