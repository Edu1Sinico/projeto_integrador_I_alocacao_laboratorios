using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaLocLab.Domain.Entities
{
    public class Laboratorios
    {
        public Guid IDLaboratorio { get; private set; }

        public int NumeroLaboratorio { get; private set; }

        public int qtdeComputador { get; private set; }

        public int capacidadeMaxAluno { get; private set; }

        public List<Software> Softwares
            { get; private set; } = new();

        public List<Alocacao> Alocacoes
            { get; private set; } = new();

        protected Laboratorios() { }

        public Laboratorios(
            int numeroLaboratorio,
            int qtdeComputador)
        {
            Validacao(numeroLaboratorio, qtdeComputador);

            IDLaboratorio = Guid.NewGuid();

            NumeroLaboratorio = numeroLaboratorio;

            this.qtdeComputador = qtdeComputador;

            capacidadeMaxAluno =
                CalcularCapacidade(qtdeComputador);
        }

        public void Atualizar(
            int numeroLaboratorio,
            int qtdeComputador)
        {
            Validacao(numeroLaboratorio, qtdeComputador);

            NumeroLaboratorio = numeroLaboratorio;

            this.qtdeComputador = qtdeComputador;

            capacidadeMaxAluno =
                CalcularCapacidade(qtdeComputador);
        }

        public bool PodeComportar(int quantidadeAlunos)
        {
            return quantidadeAlunos <= capacidadeMaxAluno;
        }

        private int CalcularCapacidade(int qtdeComputador)
        {
            return qtdeComputador * 2;
        }

        private void Validacao(
            int numeroLaboratorio,
            int qtdeComputador)
        {
            ValidacaoNumero(numeroLaboratorio);

            ValidacaoComputadores(qtdeComputador);
        }

        private void ValidacaoNumero(int numeroLaboratorio)
        {
            if (numeroLaboratorio <= 0)
                throw new ArgumentException(
                    "Número do laboratório inválido.");
        }

        private void ValidacaoComputadores(
            int qtdeComputador)
        {
            if (qtdeComputador <= 0)
                throw new ArgumentException(
                    "Quantidade de computadores inválida.");

            if (qtdeComputador > 200)
                throw new ArgumentException(
                    "Quantidade máxima excedida.");
        }
    }
}