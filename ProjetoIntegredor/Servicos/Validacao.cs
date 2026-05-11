using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;

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

        // Corrigir qualquer entrada do usuário que for incorreta.
        public static string NormalizarTexto(string texto)
        {
            return Regex.Replace(texto.Trim(), @"\s+", " ");
        }

        public static DateOnly ValidarData(string valor)
        {
            if (!DateOnly.TryParse(valor, out DateOnly data))
                throw new ArgumentException("Data inválida! Use o formato dd/mm/aaaa.");

            return data;
        }

        public static TimeOnly ValidarHora(string valor)
        {
            if (!TimeOnly.TryParse(valor, out TimeOnly hora))
                throw new ArgumentException("Horário inválido! Use o formato HH:mm.");

            return hora;
        }

        // Leitura de blocos
        public static bool TentarLerBloco(out Bloco bloco, out string mensagem) // Retornará o bloco e uma mensagem
        {
            bloco = default;
            mensagem = "";

            Console.WriteLine("\nInforme o Bloco:");
            Console.WriteLine("A");
            Console.WriteLine("B");
            Console.WriteLine("C");
            Console.WriteLine("D");

            Console.Write("\nOpção: ");
            string? opcao = Console.ReadLine()?.ToUpper().Trim();

            if (opcao == "0")
            {
                mensagem = "Operação cancelada.";
                return false;
            }

            switch (opcao)
            {
                case "A":
                    bloco = Bloco.A;
                    return true;
                case "B":
                    bloco = Bloco.B;
                    return true;
                case "C":
                    bloco = Bloco.C;
                    return true;
                case "D":
                    bloco = Bloco.D;
                    return true;
                default:
                    mensagem = "Informe um bloco válido!";
                    return false;
            }
        }
    }
}