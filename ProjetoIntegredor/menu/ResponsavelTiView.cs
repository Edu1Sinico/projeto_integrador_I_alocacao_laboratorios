using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

namespace ProjetoIntegredor.menu
{
    public class ResponsavelTiView
    {
        // Menu de Cadastro dos softwares
        public static string CadastrarSoftwareInterface(Usuario usuarioLogado, SoftwareService softwareService)
        {
            if (usuarioLogado.Tipo == TipoUsuario.RT)
            {
                while (true)
                {
                    Console.WriteLine("\n====== CADASTRAR SOFTWARE ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o nome do software: ");
                    string? nomeSoftware = Console.ReadLine();
                    if (nomeSoftware == "0") return "Cadastro cancelado.";
                    nomeSoftware = Validacao.NormalizarTexto(nomeSoftware!);

                    Console.Write("Informe a versão do software: ");
                    string? versao = Console.ReadLine();
                    if (versao == "0") return "Cadastro cancelado.";
                    versao = Validacao.NormalizarTexto(versao!);

                    try
                    {
                        var softwareCadastrado = softwareService.CadastrarSoftware(usuarioLogado, nomeSoftware!, versao!);

                        return $"Software {softwareCadastrado.NomeSoftware} cadastrado com sucesso!";
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
                return "Erro: Apenas responsáveis pelo TI podem executar essa ação!";
            }
        }

        // Menu de buscas dos softwares (Por nome e listagens completa)
        public static string BuscarSoftwareNomeInterface(SoftwareService softwareService)
        {
            Console.WriteLine("\n====== BUSCAR SOFTWARE POR NOME ======\n");
            Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

            Console.Write("Informe o nome do software: ");
            string? nomeSoftware = Console.ReadLine();
            if (nomeSoftware == "0") return "Busca cancelada.";
            nomeSoftware = Validacao.NormalizarTexto(nomeSoftware!);

            var softwareEncontrado = softwareService.BuscarSoftwareNome(nomeSoftware!);

            return FormatarSoftware(softwareEncontrado!);
        }

        public static string ListarSoftwareInterface(SoftwareService softwareService)
        {
            var softwares = softwareService.BuscarSoftwares();

            return FormatarSoftware(softwares);
        }

        // Menu de atualização dos softwares
        public static string AtualizarSoftwareInterface(Usuario usuarioLogado, SoftwareService softwareService)
        {

            int idSoftware;

            if (usuarioLogado.Tipo == TipoUsuario.RT)
            {
                while (true)
                {
                    Console.WriteLine("\n====== ATUALIZAR SOFTWARE ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o ID do software: ");
                    string? id = Console.ReadLine();
                    if (id == "0") return "Atualização cancelada.";

                    try
                    {
                        idSoftware = Validacao.ValidarInteiro(id!);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    var softwareEncontrado = softwareService.BuscarSoftwareID(idSoftware!);

                    if (softwareEncontrado != null)
                    {
                        Console.WriteLine("\n====== SOFTWARE ENCONTRADO PARA ATUALIZAÇÃO ======\n");
                        Console.WriteLine($"(ID: {softwareEncontrado.IdSoftware} - Nome: {softwareEncontrado.NomeSoftware} - Versão: {softwareEncontrado.Versao})");

                        Console.WriteLine("\nInforme os campos que deseja atualizar (deixe em branco os campos que não deseja alterar):\n");

                        Console.Write("Informe o novo nome do software: ");
                        string? nomeSoftware = Console.ReadLine();
                        if (nomeSoftware == "0") return "Atualização cancelada.";
                        if (string.IsNullOrWhiteSpace(nomeSoftware)) nomeSoftware = softwareEncontrado.NomeSoftware;
                        nomeSoftware = Validacao.NormalizarTexto(nomeSoftware!);

                        Console.Write("Informe o nova versão do software: ");
                        string? versao = Console.ReadLine();
                        if (versao == "0") return "Atualização cancelada.";
                        if (string.IsNullOrWhiteSpace(versao)) versao = softwareEncontrado.Versao;
                        versao = Validacao.NormalizarTexto(versao!);

                        try
                        {
                            var softwareAtualizado = softwareService.AtualizarSoftware(usuarioLogado, softwareEncontrado.IdSoftware, nomeSoftware!, versao!);

                            return $"Software {softwareAtualizado!.NomeSoftware} atualizado com sucesso!";
                        }
                        catch (ArgumentException ex)
                        {
                            return $"Erro ao atualizar software: {ex.Message}";
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            return $"Erro: {ex.Message}";
                        }
                    }
                    else
                        return "Software não encontrado.";
                }
            }
            else
            {
                return "Erro: Apenas responsáveis de TI podem executar essa ação!";
            }
        }

        // Menu de exclusão dos softwares
        public static string ExcluirSoftwareInterface(Usuario usuarioLogado, SoftwareService softwareService)
        {

            int idSoftware;

            if (usuarioLogado.Tipo == TipoUsuario.RT)
            {
                while (true)
                {
                    Console.WriteLine("\n====== EXCLUIR SOFTWARES ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o ID do software: ");
                    string? id = Console.ReadLine();
                    if (id == "0") return "Atualização cancelada.";

                    try
                    {
                        idSoftware = Validacao.ValidarInteiro(id!);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    var softwareEncontrado = softwareService.BuscarSoftwareID(idSoftware!);

                    if (softwareEncontrado != null)
                    {
                        Console.WriteLine("\n====== SOFTWARE ENCONTRADO PARA EXCLUSÃO ======\n");
                        Console.WriteLine($"(ID: {softwareEncontrado.IdSoftware} - Nome: {softwareEncontrado.NomeSoftware} - Versão: {softwareEncontrado.Versao})");

                        Console.WriteLine("\nDeseja realmente excluir este software? (S - Sim | N - Não)");
                        Console.Write("\nOpção: ");

                        string? opcao = Console.ReadLine()?.ToUpper().Trim();

                        switch (opcao)
                        {
                            case "S":
                                try
                                {
                                    var softwareExcluido = softwareService.ExcluirSoftware(usuarioLogado, softwareEncontrado.IdSoftware);
                                    return $"Software {softwareExcluido!.NomeSoftware} excluído com sucesso!";
                                }
                                catch (InvalidOperationException ex)
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
                        return "Software não encontrado.";
                }
            }
            else
            {
                return "Erro: Apenas responsáveis pelo TI podem executar essa ação!";
            }
        }

        // Controle das telas de softwares
        public static string CrudSoftwaresInterface(Usuario usuarioLogado, SoftwareService softwareService)
        {

            int opcaoSelecionada;

            if (usuarioLogado.Tipo == TipoUsuario.RT)
            {

                while (true)
                {
                    Console.WriteLine("\n====== FUNÇÕES DE SOFTWARE ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.WriteLine("Escolha uma função: ");
                    Console.WriteLine("1 - Cadastrar Software");
                    Console.WriteLine("2 - Buscar Software por Nome");
                    Console.WriteLine("3 - Listar Softwares");
                    Console.WriteLine("4 - Atualizar Software");
                    Console.WriteLine("5 - Excluir Software");
                    Console.WriteLine("0 - Cancelar Operação");

                    Console.Write("\nOpção: ");
                    string? opcao = Console.ReadLine()?.Trim();

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
                            return CadastrarSoftwareInterface(usuarioLogado, softwareService);

                        case 2:
                            return BuscarSoftwareNomeInterface(softwareService);

                        case 3:
                            return ListarSoftwareInterface(softwareService);

                        case 4:
                            return AtualizarSoftwareInterface(usuarioLogado, softwareService);

                        case 5:
                            return ExcluirSoftwareInterface(usuarioLogado, softwareService);

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
                return "Erro: Apenas responsáveis pelo TI podem executar essa ação!";
            }
        }

        public static string MenuBuscasSoftwareInterface(SoftwareService softwareService)
        {

            int opcaoSelecionada;

            while (true)
            {
                Console.WriteLine("\n====== LISTAGEM E BUSCA DE SOFTWARES ======\n");
                Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                Console.WriteLine("Escolha uma função: ");
                Console.WriteLine("1 - Buscar Software por Nome");
                Console.WriteLine("2 - Listar Softwares");
                Console.WriteLine("0 - Cancelar Operação");

                Console.Write("\nOpção: ");
                string? opcao = Console.ReadLine()?.Trim();

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
                        return BuscarSoftwareNomeInterface(softwareService);

                    case 2:
                        return ListarSoftwareInterface(softwareService);

                    case 0:
                        return "Operação cancelada.";

                    default:
                        Console.WriteLine("\nInforme uma opção válida!");
                        break;
                }
            }
        }

        // Outros métodos

        // Formatar listagem de Softwares
        public static string FormatarSoftware(List<Software> softwares)
        {
            if (softwares == null || softwares.Count == 0)
                return "Nenhum software encontrado.";

            string resultado = "\n====== SOFTWARES ENCONTRADOS ======\n";

            foreach (var software in softwares)
            {
                resultado += $"\n(ID: {software.IdSoftware} - Nome: {software.NomeSoftware} - Versão: {software.Versao})";
            }

            return resultado;
        }
    }
}