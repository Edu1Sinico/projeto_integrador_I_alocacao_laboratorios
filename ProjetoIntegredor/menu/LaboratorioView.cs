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
                        numLaboratorio = Validacao.ValidarInteiro(numero);
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
                        qtdeComputador = Validacao.ValidarInteiro(qtde);
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
                    string? opcao = Console.ReadLine()?.ToUpper();
                    if (opcao == "0") return "Cadastro cancelado.";

                    Bloco bloco;

                    switch (opcao)
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
                    numLaboratorio = Validacao.ValidarInteiro(numero);
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
                string? opcao = Console.ReadLine()?.ToUpper();
                if (opcao == "0") return "Cadastro cancelado.";

                Bloco bloco;

                switch (opcao)
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

                var laboratorioEncontrada = laboratorioService.BuscarLaboratorioNumBloco(numLaboratorio!, bloco);

                if (laboratorioEncontrada != null)
                {
                    Console.WriteLine("\n====== USUÁRIO ENCONTRADO ======");
                    return $"(ID: {laboratorioEncontrada.IdLaboratorio} - Número: {laboratorioEncontrada.NumLaboratorio} - Bloco: {laboratorioEncontrada.Bloco} - Qtde. de Computadores: {laboratorioEncontrada.QtdeComputador} - Capacidade Máxima de Alunos: {laboratorioEncontrada.CapacidadeMaxAluno} - Disponibilidade: {laboratorioEncontrada.StatusDisponibilidade})";
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
                    Console.WriteLine("2 - Buscar Usuário por Número e Bloco");
                    Console.WriteLine("3 - Listar Laboratórios");
                    Console.WriteLine("4 - Atualizar Laboratório");
                    Console.WriteLine("5 - Excluir Laboratório");
                    Console.WriteLine("0 - Cancelar Operação");

                    Console.Write("\nOpção: ");
                    string? opcao = Console.ReadLine();

                    try
                    {
                        opcaoSelecionada = Validacao.ValidarInteiro(opcao);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nErro: {ex.Message}");
                        continue;
                    }

                    switch (opcaoSelecionada)
                    {
                        case 1:
                            return CadLabInterface(usuarioLogado, laboratorioService);

                        case 2:
                            return BuscarLaboratorioNumBlocoInterface(laboratorioService);

                        case 3:
                            return ListarLaboratoriosInterface(laboratorioService);

                        case 4:


                        case 5:


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

        // ---------------------------------------------------------------------------------------------------------------------------

        // Vincular software com laboratório (Fazer uma opção de vincular ou remover software  )
        /*        public static string VinSoftwareLabInterface(Usuario usuarioLogado, LaboratorioService laboratorioService, List<Laboratorio> laboratorios, List<Software> softwares)
                {
                    if (usuarioLogado.Tipo == TipoUsuario.DI)
                    {

                    }
                    else
                    {
                        return "Erro: Apenas diretores podem executar essa ação!";
                    }
                } 

        */

        // Outros


        // Formatar listagem de Laboratórios
        public static string FormatarLaboratorios(List<Laboratorio> laboratorios)
        {
            if (laboratorios == null || laboratorios.Count == 0)
                return "Nenhum laboratório encontrado.";

            string resultado = "\n====== LABORATÓRIOS ENCONTRADOS ======\n";

            foreach (var laboratorio in laboratorios)
            {
                resultado += $"\n(ID: {laboratorio.IdLaboratorio} - Número: {laboratorio.NumLaboratorio} - Bloco: {laboratorio.Bloco} - Qtde. de Computadores: {laboratorio.QtdeComputador} - Capacidade Máxima de Alunos: {laboratorio.CapacidadeMaxAluno} - Disponibilidade: {laboratorio.StatusDisponibilidade})";
            }

            return resultado;
        }
    }
}