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

            int opcaoSelecionada;

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
                    string? opcao = Console.ReadLine();
                    if (opcao == "0") return "Cadastro cancelado.";

                    try
                    {
                        opcaoSelecionada = ValidarInteiro(opcao);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
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

        // Menu de Cadastro de Labotatórios
        public static string CadLabInterface(Usuario usuarioLogado, LaboratorioService labService)
        {
            int numLaboratorio;
            int qtdeComputador;

            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                // Rodando infinitamente até que o usuário encaminhe os dados corretos ou que o usuário cancele o cadastro
                while (true)
                {
                    Console.WriteLine("\n====== CADASTRAR LABORATÓRIO ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o número do laboratório: ");
                    string? numero = Console.ReadLine();
                    if (numero == "0") return "Cadastro cancelado.";

                    // Verifica se o usuário realmente digitou um número
                    try
                    {
                        numLaboratorio = ValidarInteiro(numero);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    Console.Write("Informe a quantidade de computadores: ");
                    string? qtde = Console.ReadLine();
                    if (qtde == "0") return "Cadastro cancelado.";

                    // Verifica se o usuário realmente digitou um número
                    try
                    {
                        qtdeComputador = ValidarInteiro(qtde);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    Console.WriteLine("\nInforme o Bloco: ");
                    Console.WriteLine("A");
                    Console.WriteLine("B");
                    Console.WriteLine("C");
                    Console.WriteLine("D");

                    Console.Write("\nOpção: ");
                    string? opcao = Console.ReadLine();
                    if (opcao == "0") return "Cadastro cancelado.";

                    Bloco bloco;

                    switch (opcao.ToUpper())
                    {
                        case "A":
                            bloco = Bloco.A;
                            break;
                        case "B":
                            bloco = Bloco.B;
                            break;
                        case "C":
                            bloco = Bloco.C;
                            break;
                        case "D":
                            bloco = Bloco.D;
                            break;
                        default:
                            Console.WriteLine("\nInforme um bloco válido!");
                            continue;
                    }

                    try
                    {
                        var labCadastrado = labService.CadastrarLaboratorio(usuarioLogado, numLaboratorio!, qtdeComputador!, bloco); // "!" Permite cadastrar mesmo sabendo que o valor pode ser nulo

                        return $"Laboratório cadastrado com sucesso!";
                    }
                    catch (ArgumentException ex)
                    {
                        return $"Erro ao cadastrar laboratório: {ex.Message}";
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

        // Outros métodos

        // Validar se o usuário digitou um número
        private static int ValidarInteiro(string valor)
        {
            if (!int.TryParse(valor, out int valorInformado))
                throw new ArgumentException("Digite um número válido!");
            return valorInformado;
        }
    }
}