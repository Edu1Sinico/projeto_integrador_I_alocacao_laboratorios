using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

namespace ProjetoIntegredor.menu
{
    public class LaboratorioView
    {
        // Menu de Cadastro de Labotatórios
        public static string CadastrarLaboratorioInterface(Usuario usuarioLogado, LaboratorioService LaboratorioService)
        {
            int numLaboratorio;
            int qtdeComputador;

            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                // Rodando infinitamente até que o laboratório encaminhe os dados corretos ou que o laboratório cancele o cadastro
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
                        numLaboratorio = Validacao.ValidarInteiro(numero!);
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
                        qtdeComputador = Validacao.ValidarInteiro(qtde!);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    // Verificar se o usuário escreveu o bloco corretamente.
                    if (!Validacao.TentarLerBloco(out Bloco bloco, out string mensagemBloco))
                    {
                        Console.WriteLine($"\n{mensagemBloco}");
                        continue;
                    }

                    try
                    {
                        LaboratorioService.CadastrarLaboratorio(usuarioLogado, numLaboratorio!, qtdeComputador!, bloco);
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

        // Menu de buscas dos laboratórios (por número de laboratório com bloco e listagem completa)
        public static string BuscarLaboratorioNumBlocoInterface(LaboratorioService laboratorioService)
        {
            int numLaboratorio;

            while (true)
            {
                Console.WriteLine("\n====== BUSCAR LABORATÓRIO POR NÚMERO E BLOCO ======\n");
                Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                Console.Write("Informe o número do laboratório: ");
                string? numero = Console.ReadLine();
                if (numero == "0") return "Cadastro cancelado.";

                try
                {
                    numLaboratorio = Validacao.ValidarInteiro(numero!);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nErro: {ex.Message}");
                    continue;
                }

                // Verificar se o usuário escreveu o bloco corretamente.
                if (!Validacao.TentarLerBloco(out Bloco bloco, out string mensagemBloco))
                {
                    Console.WriteLine($"\n{mensagemBloco}");
                    continue;
                }

                var laboratorioEncontrado = laboratorioService.BuscarLaboratorioNumBloco(numLaboratorio!, bloco);

                if (laboratorioEncontrado != null)
                {
                    // Verifica se a lista de softwares vinculados com o laboratório está vazia, caso contrário, junta o software com sua versão e atribui em uma variável
                    string softwares = laboratorioEncontrado.Softwares.Count == 0 ? "Nenhum software vinculado" : string.Join(", ", laboratorioEncontrado.Softwares.Select(s => $"{s.NomeSoftware} ({s.Versao})")); // string.Join("divisor","Selecionar os nome dos softwares e suas versões");

                    Console.WriteLine("\n====== LABORATÓRIO ENCONTRADO ======");
                    return $"(ID: {laboratorioEncontrado.IdLaboratorio} - Número: {laboratorioEncontrado.NumLaboratorio} - Bloco: {laboratorioEncontrado.Bloco} - Qtde. de Computadores: {laboratorioEncontrado.QtdeComputador} - Capacidade Máxima de Alunos: {laboratorioEncontrado.CapacidadeMaxAluno} - Disponibilidade: {laboratorioEncontrado.StatusDisponibilidade} - Softwares: [{softwares}])";
                }
                else
                    return "Laboratório não encontrado.";
            }
        }

        public static string ListarLaboratoriosInterface(LaboratorioService laboratorioService)
        {
            var laboratorios = laboratorioService.Buscarlaboratorios();

            return FormatarLaboratorios(laboratorios);
        }

        // Menu de atualização dos laboratórios
        public static string AtualizarLaboratorioInterface(Usuario usuarioLogado, LaboratorioService laboratorioService)
        {

            int numLaboratorio;
            int qtdeComputador;

            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                while (true)
                {
                    Console.WriteLine("\n====== ATUALIZAR LABORATÓRIO ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o número do laboratório: ");
                    string? numero = Console.ReadLine();
                    if (numero == "0") return "Cadastro cancelado.";

                    try
                    {
                        numLaboratorio = Validacao.ValidarInteiro(numero!);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    // Verificar se o usuário escreveu o bloco corretamente.
                    if (!Validacao.TentarLerBloco(out Bloco bloco, out string mensagemBloco))
                    {
                        Console.WriteLine($"\n{mensagemBloco}");
                        continue;
                    }

                    var laboratorioEncontrado = laboratorioService.BuscarLaboratorioNumBloco(numLaboratorio!, bloco!);

                    if (laboratorioEncontrado != null)
                    {
                        numero = null;

                        Console.WriteLine("\n====== LABORATÓRIO ENCONTRADO PARA ATUALIZAÇÃO ======\n");
                        Console.WriteLine($"(ID: {laboratorioEncontrado.IdLaboratorio} - Número: {laboratorioEncontrado.NumLaboratorio} - Bloco: {laboratorioEncontrado.Bloco} - Qtde. de Computadores: {laboratorioEncontrado.QtdeComputador} - Capacidade Máxima de Alunos: {laboratorioEncontrado.CapacidadeMaxAluno} - Disponibilidade: {laboratorioEncontrado.StatusDisponibilidade})");

                        Console.WriteLine("\nInforme os campos que deseja atualizar (deixe em branco os campos que não deseja alterar):\n");

                        Console.Write("Informe o novo número do laboratório: ");
                        numero = Console.ReadLine();
                        if (numero == "0") return "Cadastro cancelado.";
                        if (string.IsNullOrWhiteSpace(numero))
                        {
                            numLaboratorio = laboratorioEncontrado.NumLaboratorio;
                        }
                        else
                        {
                            try
                            {
                                numLaboratorio = Validacao.ValidarInteiro(numero!);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"\nErro: {ex.Message}");
                                continue;
                            }
                        }

                        Console.Write("Informe a nova quantidade de computadores: ");
                        string? qtde = Console.ReadLine();
                        if (qtde == "0") return "Cadastro cancelado.";
                        if (string.IsNullOrWhiteSpace(qtde))
                        {
                            qtdeComputador = laboratorioEncontrado.QtdeComputador;
                        }
                        else
                        {
                            try
                            {
                                qtdeComputador = Validacao.ValidarInteiro(qtde!);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"\nErro: {ex.Message}");
                                continue;
                            }
                        }

                        // Verificar se o usuário escreveu o bloco corretamente.
                        if (!Validacao.TentarLerBloco(out bloco, out mensagemBloco))
                        {
                            Console.WriteLine($"\n{mensagemBloco}");
                            continue;
                        }

                        try
                        {
                            laboratorioService.AtualizarLaboratorio(usuarioLogado, laboratorioEncontrado.IdLaboratorio, numLaboratorio!, qtdeComputador!, bloco!);
                            return $"laboratório atualizado com sucesso!";
                        }
                        catch (ArgumentException ex)
                        {
                            return $"Erro ao atualizar laboratório: {ex.Message}";
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            return $"Erro: {ex.Message}";
                        }
                    }
                    else
                        return "laboratório não encontrado.";
                }
            }
            else
            {
                return "Erro: Apenas diretores podem executar essa ação!";
            }
        }

        // Menu de exclusão dos laboratórios
        public static string ExcluirLaboratórioInterface(Usuario usuarioLogado, LaboratorioService laboratorioService)
        {

            int numLaboratorio;
            string opcao;

            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                while (true)
                {
                    Console.WriteLine("\n====== EXCLUIR LABORATÓRIOS ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o número do laboratório: ");
                    string? numero = Console.ReadLine();
                    if (numero == "0") return "Cadastro cancelado.";

                    try
                    {
                        numLaboratorio = Validacao.ValidarInteiro(numero!);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    // Verificar se o usuário escreveu o bloco corretamente.
                    if (!Validacao.TentarLerBloco(out Bloco bloco, out string mensagemBloco))
                    {
                        Console.WriteLine($"\n{mensagemBloco}");
                        continue;
                    }

                    var laboratorioEncontrado = laboratorioService.BuscarLaboratorioNumBloco(numLaboratorio!, bloco!);

                    if (laboratorioEncontrado != null)
                    {
                        Console.WriteLine("\n====== LABORATÓRIO ENCONTRADO PARA EXCLUSÃO ======\n");
                        Console.WriteLine($"(ID: {laboratorioEncontrado.IdLaboratorio} - Número: {laboratorioEncontrado.NumLaboratorio} - Bloco: {laboratorioEncontrado.Bloco} - Qtde. de Computadores: {laboratorioEncontrado.QtdeComputador} - Capacidade Máxima de Alunos: {laboratorioEncontrado.CapacidadeMaxAluno} - Disponibilidade: {laboratorioEncontrado.StatusDisponibilidade})");

                        Console.WriteLine("\nDeseja realmente excluir este usuário? (S - Sim | N - Não)");
                        Console.Write("\nOpção: ");
                        opcao = Console.ReadLine()!.ToUpper();

                        switch (opcao)
                        {
                            case "S":
                                laboratorioService.ExcluirLaboratorio(usuarioLogado, laboratorioEncontrado.IdLaboratorio);
                                return $"Laboratório excluído com sucesso!";

                            case "N":
                                return "Operação cancelada.";

                            default:
                                Console.WriteLine("\nInforme uma opção válida!");
                                continue;
                        }

                    }
                    else
                        return "Laboratório não encontrado.";
                }
            }
            else
            {
                return "Erro: Apenas diretores podem executar essa ação!";
            }
        }

        // Controle das telas de laboratórios
        public static string CrudLabInterface(Usuario usuarioLogado, LaboratorioService laboratorioService)
        {

            int opcaoSelecionada;

            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {

                while (true)
                {
                    Console.WriteLine("\n====== FUNÇÕES DE LABORATÓRIO ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.WriteLine("Escolha uma função: ");
                    Console.WriteLine("1 - Cadastrar Laboratório");
                    Console.WriteLine("2 - Buscar laboratório por Número e Bloco");
                    Console.WriteLine("3 - Listar Laboratórios");
                    Console.WriteLine("4 - Atualizar Laboratório");
                    Console.WriteLine("5 - Excluir Laboratório");
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
                            return CadastrarLaboratorioInterface(usuarioLogado, laboratorioService);

                        case 2:
                            return BuscarLaboratorioNumBlocoInterface(laboratorioService);

                        case 3:
                            return ListarLaboratoriosInterface(laboratorioService);

                        case 4:
                            return AtualizarLaboratorioInterface(usuarioLogado, laboratorioService);

                        case 5:
                            return ExcluirLaboratórioInterface(usuarioLogado, laboratorioService);

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

        public static string MenuBuscasLaboratorioInterface(LaboratorioService laboratorioService)
        {

            int opcaoSelecionada;

            while (true)
            {
                Console.WriteLine("\n====== LISTAGEM E BUSCA DE LABORATÓRIOS ======\n");
                Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                Console.WriteLine("Escolha uma função: ");
                Console.WriteLine("1 - Buscar laboratório por Número e Bloco");
                Console.WriteLine("2 - Listar Laboratórios");
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
                        return BuscarLaboratorioNumBlocoInterface(laboratorioService);

                    case 2:
                        return ListarLaboratoriosInterface(laboratorioService);

                    case 0:
                        return "Operação cancelada.";

                    default:
                        Console.WriteLine("\nInforme uma opção válida!");
                        break;
                }
            }
        }


        // ---------------------------------------------------------------------------------------------------------------------------

        // Vincular software com laboratório
        public static string VinSoftwareLabInterface(Usuario usuarioLogado, LaboratorioService laboratorioService, SoftwareService softwareService)
        {

            int numLaboratorio;
            int idSoftware;
            int opcaoSelecionada;

            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                while (true)
                {
                    Console.WriteLine("\n====== VINCULAR SOFTWARE COM LABORATÓRIO ======\n");
                    Console.WriteLine("Digite 0 a qualquer momento para cancelar.\n");

                    Console.Write("Informe o número do laboratório: ");
                    string? numero = Console.ReadLine();
                    if (numero == "0") return "Operação cancelado.";

                    try
                    {
                        numLaboratorio = Validacao.ValidarInteiro(numero!);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    // Verificar se o usuário escreveu o bloco corretamente.
                    if (!Validacao.TentarLerBloco(out Bloco bloco, out string mensagemBloco))
                    {
                        Console.WriteLine($"\n{mensagemBloco}");
                        continue;
                    }

                    var laboratorioEncontrado = laboratorioService.BuscarLaboratorioNumBloco(numLaboratorio!, bloco);

                    if (laboratorioEncontrado != null)
                    {
                        Console.WriteLine("\n====== LABORATÓRIO ENCONTRADO ======");
                        Console.WriteLine($"\n(ID: {laboratorioEncontrado.IdLaboratorio} - Número: {laboratorioEncontrado.NumLaboratorio} - Bloco: {laboratorioEncontrado.Bloco} - Qtde. de Computadores: {laboratorioEncontrado.QtdeComputador} - Capacidade Máxima de Alunos: {laboratorioEncontrado.CapacidadeMaxAluno} - Disponibilidade: {laboratorioEncontrado.StatusDisponibilidade})");

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
                            Console.WriteLine("1 - Vincular Softwares com Laboratório");
                            Console.WriteLine("2 - Remover Vinculo Entre Software e Laboratório ");
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
                                        var softLabVinculado = laboratorioService.AdicionarSoftwareLaboratorio(usuarioLogado, laboratorioEncontrado.IdLaboratorio, softwareEncontrado);
                                        return $"Software {softwareEncontrado.NomeSoftware} vinculado com sucesso para o laboratório {softLabVinculado!.NumLaboratorio} do bloco {softLabVinculado!.Bloco}!";
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        return $"Erro: {ex.Message}";
                                    }


                                case 2:
                                    try
                                    {
                                        var SoftLabDesvinculado = laboratorioService.RemoverSoftwareLaboratorio(usuarioLogado, laboratorioEncontrado.IdLaboratorio, softwareEncontrado);
                                        return $"Software {softwareEncontrado.NomeSoftware} desvinculado com sucesso para o laboratório {SoftLabDesvinculado!.NumLaboratorio} do bloco {SoftLabDesvinculado!.Bloco}!";
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
                        Console.WriteLine("\nLaboratório não encontrado.");
                        continue;
                    }

                }

            }
            else
            {
                return "Erro: Apenas diretores podem executar essa ação!";
            }
        }

        // Outros

        // Formatar listagem de Laboratórios
        public static string FormatarLaboratorios(List<Laboratorio> laboratorios)
        {
            if (laboratorios == null || laboratorios.Count == 0)
                return "Nenhum laboratório encontrado.";

            string resultado = "\n====== LABORATÓRIOS ENCONTRADOS ======\n";

            foreach (var laboratorio in laboratorios)
            {
                // Verifica se a lista de softwares vinculados com o laboratório está vazia, caso contrário, junta o software com sua versão e atribui em uma variável
                string softwares = laboratorio.Softwares.Count == 0 ? "Nenhum software vinculado" : string.Join(", ", laboratorio.Softwares.Select(s => $"{s.NomeSoftware} ({s.Versao})")); // string.Join("divisor","Selecionar os nome dos softwares e suas versões");

                resultado += $"\n(ID: {laboratorio.IdLaboratorio} - Número: {laboratorio.NumLaboratorio} - Bloco: {laboratorio.Bloco} - Qtde. de Computadores: {laboratorio.QtdeComputador} - Capacidade Máxima de Alunos: {laboratorio.CapacidadeMaxAluno} - Disponibilidade: {laboratorio.StatusDisponibilidade} - Softwares: [{softwares}])";
            }

            return resultado;
        }
    }
}