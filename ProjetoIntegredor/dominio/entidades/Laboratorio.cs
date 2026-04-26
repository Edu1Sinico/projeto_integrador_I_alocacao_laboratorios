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
        public Bloco Bloco { get; private set; }
        public void AlterarBloco(Bloco bloco)
        {
            Bloco = bloco;
        }
        public Disponibilidade StatusDisponibilidade { get; private set; } = Disponibilidade.D; // Define automaticamente a disponibilidade como "D - Disponível".
        public List<Software> Softwares { get; private set; } = new();

        public int NumLaboratorio
        {
            get => numLaboratorio;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Número do laboratório inválido!");
                numLaboratorio = value;
            }
        }

        public int QtdeComputador
        {
            get => qtdeComputador;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Quantidade de computadores inválida!");
                qtdeComputador = value;
            }
        }

        public int CapacidadeMaxAluno
        {
            get => capacidadeMaxAluno;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Capacidade máxima de alunos inválida!");
                capacidadeMaxAluno = value;
            }
        }

        public Laboratorio(int numLaboratorio, int qtdeComputador, int capacidadeMaxAluno, Bloco bloco)
        {
            IdLaboratorio = contador++;
            NumLaboratorio = numLaboratorio;
            QtdeComputador = qtdeComputador;
            CapacidadeMaxAluno = capacidadeMaxAluno;
            Bloco = bloco;
            StatusDisponibilidade = Disponibilidade.D;
        }

        // Adicionar Software para o Laboratório
        public void AdicionarSoftware(Software software)
        {
            if (software == null)
                throw new ArgumentNullException(nameof(software)); // trata o erro caso a referência do parâmetro for null

            if (!Softwares.Contains(software)) // Só adiciona se o laboratório não estiver com nenhum software vinculado a ela
                Softwares.Add(software);

        }

        // Remover Software do Laboratório
        public void RemoverSoftware(Software software)
        {
            Softwares.Remove(software);
        }
    }
}