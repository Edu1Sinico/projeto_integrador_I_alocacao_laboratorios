using System;
using System.Collections.Generic;

namespace SistemaLocLab.Domain.Entities
{
    public class Laboratorios
    {
        public Guid IDLaboratorio { get; private set; }
        public int NumeroLaboratorio { get; private set; }
        public string Bloco { get; private set; }
        public int QtdeComputador { get; private set; }
        public int CapacidadeMaxAluno { get; private set; }

        public List<Software> Softwares { get; private set; } = new();
        public List<Alocacao> Alocacoes { get; private set; } = new();

        protected Laboratorios()
        {
            Bloco = string.Empty;
        }

        public Laboratorios(int numeroLaboratorio, string bloco, int qtdeComputador)
        {
            Validacao(numeroLaboratorio, bloco, qtdeComputador);

            IDLaboratorio = Guid.NewGuid();
            NumeroLaboratorio = numeroLaboratorio;
            Bloco = bloco.Trim().ToUpper();
            QtdeComputador = qtdeComputador;
            CapacidadeMaxAluno = CalcularCapacidade(qtdeComputador);
        }

        public void Atualizar(int numeroLaboratorio, string bloco, int qtdeComputador)
        {
            Validacao(numeroLaboratorio, bloco, qtdeComputador);

            NumeroLaboratorio = numeroLaboratorio;
            Bloco = bloco.Trim().ToUpper();
            QtdeComputador = qtdeComputador;
            CapacidadeMaxAluno = CalcularCapacidade(qtdeComputador);
        }

        public bool PodeComportar(int quantidadeAlunos)
        {
            return quantidadeAlunos <= CapacidadeMaxAluno;
        }

        private int CalcularCapacidade(int qtdeComputador)
        {
            return qtdeComputador * 2;
        }

        private void Validacao(int numeroLaboratorio, string bloco, int qtdeComputador)
        {
            ValidacaoNumero(numeroLaboratorio);
            ValidacaoBloco(bloco);
            ValidacaoComputadores(qtdeComputador);
        }

        private void ValidacaoNumero(int numeroLaboratorio)
        {
            if (numeroLaboratorio <= 0)
                throw new ArgumentException("Numero do laboratorio invalido.");
        }

        private void ValidacaoBloco(string bloco)
        {
            if (string.IsNullOrWhiteSpace(bloco))
                throw new ArgumentException("Bloco do laboratorio obrigatorio.");

            if (bloco.Trim().Length > 20)
                throw new ArgumentException("Bloco do laboratorio muito grande.");
        }

        private void ValidacaoComputadores(int qtdeComputador)
        {
            if (qtdeComputador <= 0)
                throw new ArgumentException("Quantidade de computadores invalida.");

            if (qtdeComputador > 200)
                throw new ArgumentException("Quantidade maxima excedida.");
        }
    }
}
