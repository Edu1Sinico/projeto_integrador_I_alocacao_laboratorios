using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

namespace ProjetoIntegredor.menu
{
    public class DiretorView
    {
        // Menu de cadastro do usuário
        public static string CadUsuarioInterface(Usuario usuarioLogado, UsuarioService usuarioService)
        {
            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                // Rodando infinitamente até que o usuário encaminhe os dados corretos ou que o usuário cancele o cadastro
                while (true)
                {
                    Console.WriteLine("\n====== CADASTRAR USUÁRIO ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o RE do usuário: ");
                    string? re = Console.ReadLine();
                    if (re == "0") return "Cadastro cancelado.";

                    Console.Write("Informe o nome do usuário: ");
                    string? nome = Console.ReadLine();
                    if (nome == "0") return "Cadastro cancelado.";

                    Console.Write("Informe a senha do usuário: ");
                    string? senha = Console.ReadLine();
                    if (senha == "0") return "Cadastro cancelado.";

                    Console.Write("Informe o e-mail do usuário: ");
                    string? email = Console.ReadLine();
                    if (email == "0") return "Cadastro cancelado.";

                    Console.WriteLine("\nEscolha o tipo de usuário: ");
                    Console.WriteLine("1 - Diretor");
                    Console.WriteLine("2 - Coordenador");
                    Console.WriteLine("3 - Responsável de TI");
                    Console.Write("\nOpção: ");
                    string? opcaoTipoUsuario = Console.ReadLine();
                    if (opcaoTipoUsuario == "0") return "Cadastro cancelado.";

                    if (!int.TryParse(opcaoTipoUsuario, out int opcaoSelecionada))
                    {
                        Console.WriteLine("\nDigite uma opção válida!");
                        continue;
                    }

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

                    try
                    {
                        var usuarioCadastrado = usuarioService.CadastrarUsuario(usuarioLogado, re!, nome!, senha!, email!, tipoUsuario); // "!" Permite cadastrar mesmo sabendo que o valor pode ser nulo

                        return $"Usuário {usuarioCadastrado.Nome} cadastrado com sucesso!";
                    }
                    catch (ArgumentException ex)
                    {
                        return $"Erro ao cadastrar usuário: {ex.Message}";
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        return $"Erro: {ex.Message}";
                    }
                }
            }
            else
            {
                return "Erro: Apenas diretores podem executar essa ação!";
            }
        }
    }
}