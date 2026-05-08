using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLocLab.Domain.Entities
{
    public class laboratorios
    {
        public Guid IDLaboratorio {get; private set;}

        public int NumeroLaboratorio {get; private set;}

        public int qtdeComputador {get; private set;}

        public int capacidadeMaxAluno {get; private set;}

        public List<Software> Softwares{get; private set;} = new();

        public List<Alocacao> Alocacoes{get; private set;} = new();

        protected laboratorios(){}

        public laboratorios(int numeroLaboratorio, int QtdeComputador)
        {
            Validacao()
        }
    }
}