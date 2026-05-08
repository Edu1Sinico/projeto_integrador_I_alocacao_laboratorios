using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegredor.menu
{
    public class Validacao
    {
        // Validar se o usuário digitou um número
        public static int ValidarInteiro(string valor)
        {
            if (!int.TryParse(valor, out int valorInformado))
                throw new ArgumentException("Digite um número válido!");
            return valorInformado;
        }
    }
}