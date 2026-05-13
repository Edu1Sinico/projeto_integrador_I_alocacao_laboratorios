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
        public static Usuario? LoginInterface(UsuarioService usuarioService)
        {
            Usuario? usuarioLogado = null; // Declara o usuário que estará logado no sistema

            do
            {
                Console.WriteLine("\n====== LOGIN ======\n");

                Console.WriteLine("Selecione o usuário:");
                Console.WriteLine("1 - Diretor");
                Console.WriteLine("2 - Coordenador");
                Console.WriteLine("3 - Responsável de TI");
                Console.WriteLine("0 - Sair");
                Console.Write("\nOpção: ");

                string? opcao = Console.ReadLine()?.Trim();

                // Verifica a conversão do tipo
                if (!int.TryParse(opcao, out int opcaoSelecionada))
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
                    case 0:
                        return null;
                    default:
                        Console.WriteLine("\nInforme um usuário válido!");
                        continue;
                }

                Console.Write("\nInforme o RE Institucional: ");
                string? re = Console.ReadLine();

                Console.Write("Informe a senha: ");
                string? senha = Console.ReadLine();

                try
                {
                    usuarioLogado = usuarioService.Login(tipoUsuario, re, senha);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nErro ao realizar o login: {ex.Message}");
                    continue;
                }

            } while (usuarioLogado == null);

            return usuarioLogado;
        }

        // Após o primeiro acesso, o usuário deve obrigatoriamente alterar sua senha
        public static string NovaSenhaInterface(Usuario usuarioLogado, UsuarioService usuarioService)
        {
            while (true)
            {
                Console.WriteLine("\n====== ALTERAR SENHA ======\n");

                Console.Write("Informe a nova senha: ");
                string? novaSenha = Console.ReadLine();

                try
                {
                    usuarioService.RecuperarSenha(usuarioLogado.RE, novaSenha);
                    return "Senha alterada com sucesso!";
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nErro: {ex.Message}");
                    continue;
                }
            }
        }
    }
}