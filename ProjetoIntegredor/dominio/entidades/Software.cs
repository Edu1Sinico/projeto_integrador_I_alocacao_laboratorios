using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegredor.model
{
    public class Software
    {
        private static int contador = 1;
        public int IdSoftware { get; private set; }
        private string nomeSoftware;
        private string versao;

        public string NomeSoftware
        {
            get => nomeSoftware;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("O nome do software não pode ser vazio!");
                nomeSoftware = value;
            }
        }

        public string Versao
        {
            get => nomeSoftware;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("A versão não pode ser vazia!");
                versao = value;
            }
        }

        public Software(string nomeSoftware, string versao)
        {
            IdSoftware = contador++;
            NomeSoftware = nomeSoftware;
            Versao = versao;
        }
    }
}