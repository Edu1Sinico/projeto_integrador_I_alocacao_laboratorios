using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

namespace ProjetoIntegredor.menu
{
    public class AlocacoesView
    {
        public static string SolicitarAlocacaoInterface(Usuario usuarioLogado, AlocacaoService alocacaoService, DisciplinaService disciplinaService, LaboratorioService laboratorioService)
        {

            int numLaboratorio;
            DateOnly dataAlocacao;
            TimeOnly horarioInicio;
            TimeOnly horarioFim;

            if (usuarioLogado.Tipo == TipoUsuario.CO)
            {
                while (true)
                {
                    Console.WriteLine("\n====== SOLICITAR ALOCAÇÃO DE LABORATÓRI ======\n");
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

                    var laboratorioEncontrado = laboratorioService.BuscarLaboratorioNumBloco(numLaboratorio!, bloco!);

                    if (laboratorioEncontrado != null)
                    {

                        string softwares = laboratorioEncontrado.Softwares.Count == 0 ? "Nenhum software vinculado" : string.Join(", ", laboratorioEncontrado.Softwares.Select(s => $"{s.NomeSoftware} ({s.Versao})")); // string.Join("divisor","Selecionar os nome dos softwares e suas versões");

                        Console.WriteLine("\n====== LABORATÓRIO ENCONTRADO PARA ALOCAÇÃO ======");
                        Console.WriteLine($"(ID: {laboratorioEncontrado.IdLaboratorio} - Número: {laboratorioEncontrado.NumLaboratorio} - Bloco: {laboratorioEncontrado.Bloco} - Qtde. de Computadores: {laboratorioEncontrado.QtdeComputador} - Capacidade Máxima de Alunos: {laboratorioEncontrado.CapacidadeMaxAluno} - Disponibilidade: {laboratorioEncontrado.StatusDisponibilidade} - Softwares: [{softwares}])");

                        Console.Write("Informe o nome da disciplina: ");
                        string? nomeDisciplina = Console.ReadLine();
                        if (nomeDisciplina == "0") return "Operação cancelado.";
                        nomeDisciplina = Validacao.NormalizarTexto(nomeDisciplina!);

                        var disciplinaEncotrada = disciplinaService.BuscarDisciplinaNome(nomeDisciplina!);

                        if (disciplinaEncotrada != null)
                        {
                            softwares = disciplinaEncotrada.Softwares.Count == 0 ? "Nenhum software vinculado" : string.Join(", ", disciplinaEncotrada.Softwares.Select(s => $"{s.NomeSoftware} ({s.Versao})"));

                            Console.WriteLine("\n====== DISCIPLINA ENCONTRADA PARA ALOCAÇÃO ======");
                            Console.WriteLine($"(ID: {disciplinaEncotrada.IdDisciplina} - Disciplina: {disciplinaEncotrada.NomeDisciplina} - Qtde. de Alunos: {disciplinaEncotrada.QtdeAlunos} - Softwares: [{softwares}])");

                            Console.Write("Informe a data para alocação: ");
                            string? data = Console.ReadLine();
                            if (data == "0") return "Operação cancelado.";

                            try
                            {
                                dataAlocacao = Validacao.ValidarData(data!);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"\nErro: {ex.Message}");
                                continue;
                            }

                            Console.Write("Informe o horário inicial para alocação: ");
                            string? horaInicio = Console.ReadLine();
                            if (horaInicio == "0") return "Operação cancelado.";

                            try
                            {
                                horarioInicio = Validacao.ValidarHora(horaInicio!);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"\nErro: {ex.Message}");
                                continue;
                            }

                            Console.Write("Informe o horário final para alocação: ");
                            string? horaFim = Console.ReadLine();
                            if (horaFim == "0") return "Operação cancelado.";

                            try
                            {
                                horarioFim = Validacao.ValidarHora(horaInicio!);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"\nErro: {ex.Message}");
                                continue;
                            }

                            Console.WriteLine("\nDeseja realizar essa alocação? (S - Sim | N - Não)");
                            Console.Write("\nOpção: ");

                            string? opcao = Console.ReadLine()?.ToUpper().Trim();

                            switch (opcao)
                            {
                                case "S":
                                    try
                                    {
                                        var laboratorioAlocado = alocacaoService.SolicitarAlocacao(usuarioLogado, disciplinaEncotrada, laboratorioEncontrado, dataAlocacao, horarioInicio, horarioFim);
                                        return $"Solicitação de alocacação para o laboratório {laboratorioAlocado!.Laboratorio.NumLaboratorio} do bloco {laboratorioAlocado!.Laboratorio.Bloco} realizada com sucesso para à disciplina {laboratorioAlocado!.Disciplina.NomeDisciplina}!";
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Console.WriteLine($"Erro: {ex.Message}");
                                        break;
                                    }
                                case "N":
                                    return "Operação cancelada.";

                                default:
                                    Console.WriteLine("\nInforme uma opção válida!");
                                    continue;
                            }

                        }
                        else
                        {
                            Console.WriteLine("\nDisciplina não encontrada.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("laboratório não encontrado.");
                        continue;
                    }

                }
            }
            else
            {
                return "Erro: Apenas coordenadores podem executar essa ação!";
            }
        }

        // Menu de histórico de alocações
        public static string HistoricoAlocacaoInterface(AlocacaoService alocacaoService)
        {
            var alocacoes = alocacaoService.HistoricoAlocacao();

            return FormatarAlocacoes(alocacoes);
        }

        // Outros métodos

        // Formatar histórico de alocações
        public static string FormatarAlocacoes(List<Alocacao> alocacoes)
        {
            if (alocacoes == null || alocacoes.Count == 0)
                return "Nenhuma alocação encontrado.";

            string resultado = "\n====== ALOCAÇÕES ENCONTRADOS ======\n";

            foreach (var alocacao in alocacoes)
            {
                resultado += $"\n(ID: {alocacao.IdAlocacao} - Laboratório: [Número: {alocacao.Laboratorio.NumLaboratorio} - Bloco: {alocacao.Laboratorio.Bloco}] - Disciplina: {alocacao.Disciplina.NomeDisciplina} - Solicitação do Usuário: {alocacao.Usuario.Nome} - Data da Alocação: {alocacao.Data} - Horário Inicial: {alocacao.HoraInicio} - Horário Final: {alocacao.HoraFim} - Aprovação: {alocacao.StatusAprovacao})";
            }

            return resultado;
        }
    }
}