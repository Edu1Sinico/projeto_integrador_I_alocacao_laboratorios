using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

namespace ProjetoIntegredor.menu
{
    public class LoginView
    {
        // Menu de Login do sistema
        public static Usuario LoginInterface(UsuarioService usuarioService)
        {
            Usuario? usuarioLogado = null; // Declara o usuário que estará logado no sistema

            do
            {
                Console.WriteLine("\n====== LOGIN ======\n");

                Console.WriteLine("Selecione o usuário:");
                Console.WriteLine("1 - Diretor");
                Console.WriteLine("2 - Coordenador");
                Console.WriteLine("3 - Responsável de TI");
                Console.Write("\nOpção: ");

                string? opcaousuarioService = Console.ReadLine();

                // Verifica a conversão do tipo
                if (!int.TryParse(opcaousuarioService, out int opcaoSelecionada))
                {
                    Console.WriteLine("\nDigite uma opção válida!");
                    continue;
                }

                // Define uma variável para o enumerador (TipoUsuario)
                TipoUsuario tipoUsuario;

                switch (opcaoSelecionada)
                {
                    case 1:
                        tipoUsuario = TipoUsuario.DI;
                        break;
                    case 2:
                        tipoUsuario = TipoUsuario.CO;
                        break;
                    case 3:
                        tipoUsuario = TipoUsuario.RT;
                        break;
                    default:
                        Console.WriteLine("\nInforme um usuário válido!");
                        continue;
                }

                Console.Write("Informe o RE Institucional: ");
                string? re = Console.ReadLine();

                Console.Write("Informe a senha: ");
                string? senha = Console.ReadLine();

                try
                {
                    usuarioLogado = usuarioService.Login(tipoUsuario, re, senha);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erro ao realizar o login: \n{ex.Message}");
                    continue;
                }

            } while (usuarioLogado == null);

            return usuarioLogado;
        }
    }
}