using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaLocLab.Domain.Entities
{
    public class Laboratorios
    {
        //Encapsulamento
        public Guid IDLaboratorio { get; private set; }

        public int NumeroLaboratorio { get; private set; }

        public int qtdeComputador { get; private set; }

        public int capacidadeMaxAluno { get; private set; }

        //Referencia as classes Software(Softwares cadastrados em cada lab) e alocacoes(Informações da alocação do lab)
        public List<Software> Softwares
            { get; private set; } = new();

        public List<Alocacao> Alocacoes
            { get; private set; } = new();

        //preteção do construtor
        protected Laboratorios() { }

        //Construtor
        public Laboratorios(int numeroLaboratorio, int qtdeComputador)
        {
            Validacao(numeroLaboratorio, qtdeComputador);

            IDLaboratorio = Guid.NewGuid();

            NumeroLaboratorio = numeroLaboratorio;

            this.qtdeComputador = qtdeComputador;

            capacidadeMaxAluno = CalcularCapacidade(qtdeComputador);
        }
//Metodo de padronização para dar update nos dados dos atributos
        public void Atualizar(int numeroLaboratorio, int qtdeComputador)
        {
            Validacao(numeroLaboratorio, qtdeComputador);

            NumeroLaboratorio = numeroLaboratorio;

            this.qtdeComputador = qtdeComputador;

            capacidadeMaxAluno = CalcularCapacidade(qtdeComputador);
        }

        //Metodo para validar se a sala comporta ou não a quantidade de alunos informada
        public bool PodeComportar(int quantidadeAlunos)
        {
            return quantidadeAlunos <= capacidadeMaxAluno;
        }

        //Faz o calculo da capacidade maxima (2 alunos por pc)
        private int CalcularCapacidade(int qtdeComputador)
        {
            return qtdeComputador * 2;
        }

        //Metodos de validação 
        private void Validacao(int numeroLaboratorio, int qtdeComputador)
        {
            ValidacaoNumero(numeroLaboratorio);

            ValidacaoComputadores(qtdeComputador);
        }

        //Numero do laboratorio não pode ser < 0
        private void ValidacaoNumero(int numeroLaboratorio)
        {
            if (numeroLaboratorio <= 0)
                throw new ArgumentException(
                    "Número do laboratório inválido.");
        }

        
        private void ValidacaoComputadores(int qtdeComputador)
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