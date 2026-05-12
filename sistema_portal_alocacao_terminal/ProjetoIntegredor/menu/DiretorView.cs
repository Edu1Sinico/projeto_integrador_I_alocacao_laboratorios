using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

namespace ProjetoIntegredor.menu
{
    public class DiretorView
    {
        // Menu de cadastro do usuário
        public static string CadastrarUsuarioInterface(Usuario usuarioLogado, UsuarioService usuarioService)
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
                    re = Validacao.NormalizarTexto(re!);

                    Console.Write("Informe o nome do usuário: ");
                    string? nome = Console.ReadLine();
                    if (nome == "0") return "Cadastro cancelado.";
                    nome = Validacao.NormalizarTexto(nome!);

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
                    string? opcao = Console.ReadLine()!.Trim();
                    if (opcao == "0") return "Cadastro cancelado.";

                    try
                    {
                        opcaoSelecionada = Validacao.ValidarInteiro(opcao!);
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
                        var usuarioCadastrado = usuarioService.CadastrarUsuario(usuarioLogado, re!, nome!, senha!, email!.Trim().ToLower(), tipoUsuario); // "!" Permite cadastrar mesmo sabendo que o valor pode ser nulo

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

        // Menu de buscas dos usuários (por re, nome e listagem completa)
        public static string BuscarUsuarioReInterface(Usuario usuarioLogado, UsuarioService usuarioService)
        {
            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                Console.WriteLine("\n====== BUSCAR USUÁRIO POR RE ======\n");
                Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                Console.Write("Informe o RE do usuário: ");
                string? re = Console.ReadLine();
                if (re == "0") return "Busca cancelada.";
                re = Validacao.NormalizarTexto(re!);

                var usuarioEncotrado = usuarioService.BuscarUsuarioRE(usuarioLogado, re!);

                if (usuarioEncotrado != null)
                {
                    Console.WriteLine("\n====== USUÁRIO ENCONTRADO ======");
                    return $"(ID: {usuarioEncotrado.IdUsuario} - RE: {usuarioEncotrado.RE} - Nome: {usuarioEncotrado.Nome} - E-mail: {usuarioEncotrado.Email} - Tipo: {usuarioEncotrado.Tipo})";
                }
                else
                    return "Usuário não encontrado.";
            }
            else
            {
                return "Erro: Apenas diretores podem executar essa ação!";
            }

        }

        public static string BuscarUsuarioNomeInterface(Usuario usuarioLogado, UsuarioService usuarioService)
        {
            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                Console.WriteLine("\n====== BUSCAR USUÁRIO POR NOME ======\n");
                Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                Console.Write("Informe o nome do usuário: ");
                string? nome = Console.ReadLine();
                if (nome == "0") return "Busca cancelada.";
                nome = Validacao.NormalizarTexto(nome!);

                var usuariosEncotrados = usuarioService.BuscarUsuarioNome(usuarioLogado, nome!);

                return FormatarUsuarios(usuariosEncotrados);
            }
            else
            {
                return "Erro: Apenas diretores podem executar essa ação!";
            }

        }

        public static string ListarUsuariosInterface(Usuario usuarioLogado, UsuarioService usuarioService)
        {
            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                var usuarios = usuarioService.BuscarUsuarios(usuarioLogado);

                return FormatarUsuarios(usuarios);
            }
            else
            {
                return "Erro: Apenas diretores podem executar essa ação!";
            }
        }

        // Menu de atualização dos usuários
        public static string AtualizarUsuarioInterface(Usuario usuarioLogado, UsuarioService usuarioService)
        {
            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                while (true)
                {
                    Console.WriteLine("\n====== ATUALIZAR USUÁRIO ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o RE do usuário: ");
                    string? re = Console.ReadLine();
                    if (re == "0") return "Atualização cancelada.";
                    re = Validacao.NormalizarTexto(re!);

                    var usuarioEncotrado = usuarioService.BuscarUsuarioRE(usuarioLogado, re!);

                    if (usuarioEncotrado != null)
                    {
                        Console.WriteLine("\n====== USUÁRIO ENCONTRADO PARA ATUALIZAÇÃO ======\n");
                        Console.WriteLine($"(ID: {usuarioEncotrado.IdUsuario} - RE: {usuarioEncotrado.RE} - Nome: {usuarioEncotrado.Nome} - E-mail: {usuarioEncotrado.Email} - Tipo: {usuarioEncotrado.Tipo})");

                        Console.WriteLine("\nInforme os campos que deseja atualizar (deixe em branco os campos que não deseja alterar):\n");

                        Console.Write("Informe o novo nome do usuário: ");
                        string? nome = Console.ReadLine();
                        if (nome == "0") return "Atualização cancelada.";
                        if (string.IsNullOrWhiteSpace(nome)) nome = usuarioEncotrado.Nome; // Caso o valor esteja vazio, ele mantem o valor original.
                        nome = Validacao.NormalizarTexto(nome!);

                        Console.Write("Informe o novo e-mail do usuário: ");
                        string? email = Console.ReadLine();
                        if (email == "0") return "Atualização cancelada.";
                        if (string.IsNullOrWhiteSpace(email)) email = usuarioEncotrado.Email; // Caso o valor esteja vazio, ele mantem o valor original.

                        Console.Write("Informe a nova senha do usuário: ");
                        string? senha = Console.ReadLine();
                        if (senha == "0") return "Atualização cancelada.";
                        if (string.IsNullOrWhiteSpace(senha)) senha = null; // Caso o valor esteja vazio, ele mantem o valor original e é validado dentro do método "AtualizarSoftware".

                        try
                        {
                            var usuarioAtualizado = usuarioService.AtualizarUsuario(usuarioLogado, usuarioEncotrado.IdUsuario, nome!, email!.Trim().ToLower(), senha!.Trim());

                            return $"Usuário {usuarioAtualizado!.Nome} atualizado com sucesso!";
                        }
                        catch (ArgumentException ex)
                        {
                            return $"Erro ao atualizar usuário: {ex.Message}";
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            return $"Erro: {ex.Message}";
                        }
                    }
                    else
                        return "Usuário não encontrado.";
                }
            }
            else
            {
                return "Erro: Apenas diretores podem executar essa ação!";
            }
        }

        // Menu de exclusão dos usuários
        public static string ExcluirUsuarioInterface(Usuario usuarioLogado, UsuarioService usuarioService)
        {
            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                while (true)
                {
                    Console.WriteLine("\n====== EXCLUIR USUÁRIOS ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o RE do usuário: ");
                    string? re = Console.ReadLine();
                    if (re == "0") return "Exclusão cancelada.";
                    re = Validacao.NormalizarTexto(re!);

                    var usuarioEncotrado = usuarioService.BuscarUsuarioRE(usuarioLogado, re!);

                    if (usuarioEncotrado != null)
                    {
                        Console.WriteLine("\n====== USUÁRIO ENCONTRADO PARA EXCLUSÃO ======\n");
                        Console.WriteLine($"(ID: {usuarioEncotrado.IdUsuario} - RE: {usuarioEncotrado.RE} - Nome: {usuarioEncotrado.Nome} - E-mail: {usuarioEncotrado.Email} - Tipo: {usuarioEncotrado.Tipo})");

                        Console.WriteLine("\nDeseja realmente excluir este usuário? (S - Sim | N - Não)");
                        Console.Write("\nOpção: ");
                        string? opcao = Console.ReadLine()?.ToUpper().Trim();

                        switch (opcao)
                        {
                            case "S":
                                try
                                {
                                    var usuarioExcluido = usuarioService.ExcluirUsuario(usuarioLogado, usuarioEncotrado.IdUsuario);
                                    return $"Usuário {usuarioExcluido!.Nome} excluído com sucesso!";
                                }
                                catch (InvalidOperationException ex) // Tratando o erro se o usuário digitar o próprio ID para exclusão.
                                {
                                    return $"Erro: {ex.Message}";
                                }

                            case "N":
                                return "Operação cancelada.";

                            default:
                                Console.WriteLine("\nInforme uma opção válida!");
                                continue;
                        }

                    }
                    else
                        return "Usuário não encontrado.";
                }
            }
            else
            {
                return "Erro: Apenas diretores podem executar essa ação!";
            }
        }

        // Controle das telas de usuários
        public static string CrudUsuariosInterface(Usuario usuarioLogado, UsuarioService usuarioService)
        {

            int opcaoSelecionada;

            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {

                while (true)
                {
                    Console.WriteLine("\n====== FUNÇÕES DE USUÁRIO ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.WriteLine("Escolha uma função: ");
                    Console.WriteLine("1 - Cadastrar Usuário");
                    Console.WriteLine("2 - Buscar Usuário por RE");
                    Console.WriteLine("3 - Buscar Usuário por Nome");
                    Console.WriteLine("4 - Listar Usuários");
                    Console.WriteLine("5 - Atualizar Usuário");
                    Console.WriteLine("6 - Excluir Usuário");
                    Console.WriteLine("0 - Cancelar Operação");

                    Console.Write("\nOpção: ");
                    string? opcao = Console.ReadLine()!.Trim();

                    try
                    {
                        opcaoSelecionada = Validacao.ValidarInteiro(opcao!);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    switch (opcaoSelecionada)
                    {
                        case 1:
                            return CadastrarUsuarioInterface(usuarioLogado, usuarioService);

                        case 2:
                            return BuscarUsuarioReInterface(usuarioLogado, usuarioService);

                        case 3:
                            return BuscarUsuarioNomeInterface(usuarioLogado, usuarioService);

                        case 4:
                            return ListarUsuariosInterface(usuarioLogado, usuarioService);

                        case 5:
                            return AtualizarUsuarioInterface(usuarioLogado, usuarioService);

                        case 6:
                            return ExcluirUsuarioInterface(usuarioLogado, usuarioService);

                        case 0:
                            return "Operação cancelada.";

                        default:
                            Console.WriteLine("\nInforme uma opção válida!");
                            break;
                    }
                }
            }
            else
            {
                return "Erro: Apenas diretores podem executar essa ação!";
            }
        }

        // Outros métodos

        // Formatar listagem de Usuários
        public static string FormatarUsuarios(List<Usuario> usuarios)
        {
            if (usuarios == null || usuarios.Count == 0)
                return "Nenhum usuário encontrado.";

            string resultado = "\n====== USUÁRIOS ENCONTRADOS ======\n";

            foreach (var usuario in usuarios)
            {
                resultado += $"\n(ID: {usuario.IdUsuario} - RE: {usuario.RE} - Nome: {usuario.Nome} - E-mail: {usuario.Email} - Tipo: {usuario.Tipo})";
            }

            return resultado;
        }
    }
}