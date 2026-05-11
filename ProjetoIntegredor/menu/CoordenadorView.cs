using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

namespace ProjetoIntegredor.menu
{
    public class CoordenadorView
    {
        // Menu de Cadastro de Disciplinas
        public static string CadastrarDisciplinaInterface(Usuario usuarioLogado, DisciplinaService disciplinaService)
        {

            int qtdeAlunos;

            if (usuarioLogado.Tipo == TipoUsuario.CO)
            {
                while (true)
                {
                    Console.WriteLine("\n====== CADASTRAR DISCIPLINAS ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o nome da disciplina: ");
                    string? nomeDisciplina = Console.ReadLine();
                    if (nomeDisciplina == "0") return "Cadastro cancelado.";
                    nomeDisciplina = Validacao.NormalizarTexto(nomeDisciplina!);

                    Console.Write("Informe a quantidade de alunos: ");
                    string? qtde = Console.ReadLine();
                    if (qtde == "0") return "Cadastro cancelado.";

                    // Verifica se o usuário realmente digitou um número
                    try
                    {
                        qtdeAlunos = Validacao.ValidarInteiro(qtde!);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    try
                    {
                        var disciplinaCadastrada = disciplinaService.CadastrarDisciplina(usuarioLogado, nomeDisciplina!, qtdeAlunos!);
                        return $"Disciplina {disciplinaCadastrada.NomeDisciplina} cadastrado com sucesso!";
                    }
                    catch (ArgumentException ex)
                    {
                        return $"Erro ao cadastrar disciplina: {ex.Message}";
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        return $"Erro: {ex.Message}";
                    }
                }
            }
            else
            {
                return "Erro: Apenas coordenadores podem executar essa ação!";
            }
        }

        // Menu de buscas das disciplinas (Por nome e listagens completa)
        public static string BuscarDisciplinaNomeInterface(DisciplinaService disciplinaService)
        {
            Console.WriteLine("\n====== BUSCAR DISCIPLINA POR NOME ======\n");
            Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

            Console.Write("Informe o nome da disciplina: ");
            string? nomeDisciplina = Console.ReadLine();
            if (nomeDisciplina == "0") return "Cadastro cancelado.";
            nomeDisciplina = Validacao.NormalizarTexto(nomeDisciplina!);

            var disciplinaEncotrada = disciplinaService.BuscarDisciplinaNome(nomeDisciplina!);

            if (disciplinaEncotrada != null)
            {
                string softwares = disciplinaEncotrada.Softwares.Count == 0 ? "Nenhum software vinculado" : string.Join(", ", disciplinaEncotrada.Softwares.Select(s => $"{s.NomeSoftware} ({s.Versao})"));

                Console.WriteLine("\n====== DISCIPLINA ENCONTRADA ======");
                return $"(ID: {disciplinaEncotrada.IdDisciplina} - Disciplina: {disciplinaEncotrada.NomeDisciplina} - Qtde. de Alunos: {disciplinaEncotrada.QtdeAlunos} - Softwares: [{softwares}])";
            }
            else
                return "Disciplina não encontrada.";
        }

        public static string ListarDisciplinasInterface(DisciplinaService disciplinaService)
        {
            var disciplinas = disciplinaService.BuscarDisciplinas();

            return FormatarDisciplinas(disciplinas);
        }

        // Menu de atualização das disciplinas
        public static string AtualizarDisciplinaInterface(Usuario usuarioLogado, DisciplinaService disciplinaService)
        {

            int qtdeAlunos;

            if (usuarioLogado.Tipo == TipoUsuario.CO)
            {
                while (true)
                {
                    Console.WriteLine("\n====== ATUALIZAR DISCIPLINA ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o nome da disciplina: ");
                    string? nomeDisciplina = Console.ReadLine();
                    if (nomeDisciplina == "0") return "Operação cancelado.";
                    nomeDisciplina = Validacao.NormalizarTexto(nomeDisciplina!);

                    var disciplinaEncotrada = disciplinaService.BuscarDisciplinaNome(nomeDisciplina!);

                    if (disciplinaEncotrada != null)
                    {
                        Console.WriteLine("\n====== DISCIPLINA ENCONTRADO PARA ATUALIZAÇÃO ======\n");
                        Console.WriteLine($"(ID: {disciplinaEncotrada.IdDisciplina} - Disciplina: {disciplinaEncotrada.NomeDisciplina} - Qtde. de Alunos: {disciplinaEncotrada.QtdeAlunos})");

                        Console.WriteLine("\nInforme os campos que deseja atualizar (deixe em branco os campos que não deseja alterar):\n");

                        Console.Write("Informe o novo nome da disciplina: ");
                        nomeDisciplina = Console.ReadLine();
                        if (nomeDisciplina == "0") return "Operação cancelado.";
                        nomeDisciplina = Validacao.NormalizarTexto(nomeDisciplina!);

                        Console.Write("Informe a nova quantidade de alunos: ");
                        string? qtde = Console.ReadLine();
                        if (nomeDisciplina == "0") return "Operação cancelado.";
                        if (string.IsNullOrWhiteSpace(qtde))
                        {
                            qtdeAlunos = disciplinaEncotrada.QtdeAlunos;
                        }
                        else
                        {
                            try
                            {
                                qtdeAlunos = Validacao.ValidarInteiro(qtde!);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"\nErro: {ex.Message}");
                                continue;
                            }
                        }

                        try
                        {
                            var disciplinaAtualizado = disciplinaService.AtualizarDisciplina(usuarioLogado, disciplinaEncotrada.IdDisciplina, nomeDisciplina!, qtdeAlunos!);

                            return $"Disciplina {disciplinaAtualizado!.NomeDisciplina} atualizado com sucesso!";
                        }
                        catch (ArgumentException ex)
                        {
                            return $"Erro ao atualizar disciplina: {ex.Message}";
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            return $"Erro: {ex.Message}";
                        }
                    }
                    else
                        return "Disciplina não encontrada.";
                }
            }
            else
            {
                return "Erro: Apenas coordenadores podem executar essa ação!";
            }
        }

        // Menu de exclusão das disciplinas
        public static string ExcluirDisciplinaInterface(Usuario usuarioLogado, DisciplinaService disciplinaService)
        {

            if (usuarioLogado.Tipo == TipoUsuario.CO)
            {
                while (true)
                {
                    Console.WriteLine("\n====== EXCLUIR DISCIPLINAS ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o nome da disciplina: ");
                    string? nomeDisciplina = Console.ReadLine();
                    if (nomeDisciplina == "0") return "Operação cancelado.";
                    nomeDisciplina = Validacao.NormalizarTexto(nomeDisciplina!);

                    var disciplinaEncotrada = disciplinaService.BuscarDisciplinaNome(nomeDisciplina!);

                    if (disciplinaEncotrada != null)
                    {
                        Console.WriteLine("\n====== DISCIPLINA ENCONTRADO PARA EXCLUSÃO ======\n");
                        Console.WriteLine($"(ID: {disciplinaEncotrada.IdDisciplina} - Disciplina: {disciplinaEncotrada.NomeDisciplina} - Qtde. de Alunos: {disciplinaEncotrada.QtdeAlunos})");

                        Console.WriteLine("\nDeseja realmente excluir esta disciplina? (S - Sim | N - Não)");
                        Console.Write("\nOpção: ");

                        string? opcao = Console.ReadLine()?.ToUpper().Trim();

                        switch (opcao)
                        {
                            case "S":
                                try
                                {
                                    var disciplinaExcluida = disciplinaService.ExcluirDisciplina(usuarioLogado, disciplinaEncotrada.IdDisciplina);
                                    return $"Disciplina {disciplinaExcluida!.NomeDisciplina} excluída com sucesso!";
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
                        return "Disciplina não encontrada.";
                }
            }
            else
            {
                return "Erro: Apenas coordenadores podem executar essa ação!";
            }
        }

        // Controle das telas de softwares
        public static string CrudDisciplinasInterface(Usuario usuarioLogado, DisciplinaService disciplinaService)
        {

            int opcaoSelecionada;

            if (usuarioLogado.Tipo == TipoUsuario.CO)
            {

                while (true)
                {
                    Console.WriteLine("\n====== FUNÇÕES DE DISCIPLINA ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.WriteLine("Escolha uma função: ");
                    Console.WriteLine("1 - Cadastrar Disciplina");
                    Console.WriteLine("2 - Buscar Disciplina por Nome");
                    Console.WriteLine("3 - Listar Disciplinas");
                    Console.WriteLine("4 - Atualizar Disciplina");
                    Console.WriteLine("5 - Excluir Discoplina");
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
                            return CadastrarDisciplinaInterface(usuarioLogado, disciplinaService);

                        case 2:
                            return BuscarDisciplinaNomeInterface(disciplinaService);

                        case 3:
                            return ListarDisciplinasInterface(disciplinaService);

                        case 4:
                            return AtualizarDisciplinaInterface(usuarioLogado, disciplinaService);

                        case 5:
                            return ExcluirDisciplinaInterface(usuarioLogado, disciplinaService);

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
                return "Erro: Apenas coordenadores podem executar essa ação!";
            }
        }

        public static string MenuBuscasDisciplinaInterface(DisciplinaService disciplinaService)
        {

            int opcaoSelecionada;

            while (true)
            {
                Console.WriteLine("\n====== LISTAGEM E BUSCA DE DISCIPLINAS ======\n");
                Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                Console.WriteLine("Escolha uma função: ");
                Console.WriteLine("1 - Buscar disciplina por nome");
                Console.WriteLine("2 - Listar Disciplinas");
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
                        return BuscarDisciplinaNomeInterface(disciplinaService);

                    case 2:
                        return ListarDisciplinasInterface(disciplinaService);

                    case 0:
                        return "Operação cancelada.";

                    default:
                        Console.WriteLine("\nInforme uma opção válida!");
                        break;
                }
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------

        // Vincular software com disciplina
        public static string VinSoftwareDiscInterface(Usuario usuarioLogado, DisciplinaService disciplinaService, SoftwareService softwareService)
        {

            int idSoftware;
            int opcaoSelecionada;

            if (usuarioLogado.Tipo == TipoUsuario.CO)
            {
                while (true)
                {
                    Console.WriteLine("\n====== VINCULAR SOFTWARE COM DISCIPLINA ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o nome da disciplina: ");
                    string? nomeDisciplina = Console.ReadLine();
                    if (nomeDisciplina == "0") return "Cadastro cancelado.";
                    nomeDisciplina = Validacao.NormalizarTexto(nomeDisciplina!);

                    var disciplinaEncotrada = disciplinaService.BuscarDisciplinaNome(nomeDisciplina!);

                    if (disciplinaEncotrada != null)
                    {
                        Console.WriteLine("\n====== DISCIPLINA ENCONTRADO ======");
                        Console.WriteLine($"(ID: {disciplinaEncotrada.IdDisciplina} - Disciplina: {disciplinaEncotrada.NomeDisciplina} - Qtde. de Alunos: {disciplinaEncotrada.QtdeAlunos})");

                        Console.Write("\nInforme o ID do software: ");
                        string? id = Console.ReadLine();
                        if (id == "0") return "Operação cancelada.";

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
                            Console.WriteLine("\nEscolha uma função: ");
                            Console.WriteLine("1 - Vincular Softwares com Disciplina");
                            Console.WriteLine("2 - Remover Vinculo Entre Software e Disciplina ");
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
                                    try
                                    {
                                        var softDisciVinculado = disciplinaService.AdicionarSoftwareDisciplina(usuarioLogado, disciplinaEncotrada.IdDisciplina, softwareEncontrado);
                                        return $"Software {softwareEncontrado.NomeSoftware} vinculado com sucesso para a disciplina {softDisciVinculado!.NomeDisciplina}!";
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        return $"Erro: {ex.Message}";
                                    }


                                case 2:
                                    try
                                    {
                                        var softDisciVinculado = disciplinaService.RemoverSoftwareDisciplina(usuarioLogado, disciplinaEncotrada.IdDisciplina, softwareEncontrado);
                                        return $"Software {softwareEncontrado.NomeSoftware} desvinculado com sucesso para a disciplina {softDisciVinculado!.NomeDisciplina}!";
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        return $"Erro: {ex.Message}";
                                    }

                                case 0:
                                    return "Operação cancelada.";

                                default:
                                    Console.WriteLine("\nInforme uma opção válida!");
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("\nSoftware não encontrado.");
                    }
                    else
                    {
                        Console.WriteLine("\nDisciplina não encontrada.");
                        continue;
                    }

                }

            }
            else
            {
                return "Erro: Apenas coordenadores podem executar essa ação!";
            }
        }


        // Outros

        // Formatar listagem de Disciplinas
        public static string FormatarDisciplinas(List<Disciplina> disciplinas)
        {
            if (disciplinas == null || disciplinas.Count == 0)
                return "Nenhuma disciplina encontrada.";

            string resultado = "\n====== DISCIPLINAS ENCONTRADAS ======\n";

            foreach (var disciplina in disciplinas)
            {
                // Verifica se a lista de softwares vinculados com a disciplina está vazia, caso contrário, junta o software com sua versão e atribui em uma variável
                string softwares = disciplina.Softwares.Count == 0 ? "Nenhum software vinculado" : string.Join(", ", disciplina.Softwares.Select(s => $"{s.NomeSoftware} ({s.Versao})")); // string.Join("divisor","Selecionar os nome dos softwares e suas versões");

                resultado += $"\n(ID: {disciplina.IdDisciplina} - Disciplina: {disciplina.NomeDisciplina} - Qtde. de Alunos: {disciplina.QtdeAlunos} - Softwares: [{softwares}])";
            }
            return resultado;
        }
    }
}