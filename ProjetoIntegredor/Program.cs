using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.menu;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

internal class Program
{
    public static void Main(string[] args)
    {
        string? opcao = null; // Variável de controle
        string? mensagem = null;
        bool sistemaAtivo = true; // Controla o sistema

        // Funcionalidades dos Usuários
        UsuarioService usuarioService = new UsuarioService(); // Chama as funcionalidades do sistema
        usuarioService.CadastrarDiretor(); // Cadastra o diretor de forma temporária

        // Funcionalidades dos Labotatórios
        LaboratorioService laboratorioService = new LaboratorioService();

        // Funcionalidades dos Softwares
        SoftwareService softwareService = new SoftwareService();

        while (sistemaAtivo)
        {
            // Definindo as telas
            Usuario? usuarioLogado = LoginView.LoginInterface(usuarioService);

            Console.WriteLine($"\nBem-vindo, {usuarioLogado.Nome}!");

            bool usuarioAutenticado = true; // controla a autenticação

            while (usuarioAutenticado)
            {
                Console.WriteLine("\n======== PORTAL DE ALOCAÇÃO ========\n");

                if (usuarioLogado.Tipo == TipoUsuario.DI)
                {
                    Console.WriteLine("Escolha uma opção:");
                    Console.WriteLine("1 - Funções de Usuários (Cadastrar, Buscar, Atualizar e Excluir)");
                    Console.WriteLine("2 - Funções de Laboratórios (Cadastrar, Buscar, Atualizar e Excluir)");
                    Console.WriteLine("3 - Vincular Software com Laboratório");
                    Console.WriteLine("4 - Aprovar/Recusar Alocação"); // Inserir histórico de alocações junto
                    Console.WriteLine("5 - Listar e Buscar Disciplinas");
                    Console.WriteLine("6 - Listar e Buscar Softwares");
                    Console.WriteLine("7 - Logout");
                    Console.WriteLine("0 - Sair");
                    Console.Write("\nOpção: ");

                    opcao = Console.ReadLine();

                    // Verifica a conversão do tipo
                    if (!int.TryParse(opcao, out int opcaoSelecionada))
                    {
                        Console.WriteLine("\nDigite uma opção válida!");
                        continue;
                    }

                    switch (opcaoSelecionada)
                    {
                        case 1:
                            mensagem = DiretorView.CrudUsuariosInterface(usuarioLogado, usuarioService);
                            Console.WriteLine($"\n{mensagem}");
                            break;
                        case 2:
                            mensagem = LaboratorioView.CrudLabInterface(usuarioLogado, laboratorioService);
                            Console.WriteLine($"\n{mensagem}");
                            break;
                        case 3:

                            break;
                        case 4:

                            break;
                        case 5:

                            break;
                        case 6:
                            mensagem = ResponsavelTiView.MenuBuscasSoftwareInterface(softwareService);
                            Console.WriteLine($"\n{mensagem}");
                            break;
                        case 7:
                            usuarioAutenticado = false;
                            Console.WriteLine("\nLogout realizado com sucesso!");
                            break;
                        case 0:
                            usuarioAutenticado = false;
                            sistemaAtivo = false;
                            Console.WriteLine("\nSistema encerrado.\n");
                            break;
                        default:
                            Console.WriteLine("\nInforme um usuário válido!");
                            break;
                    }
                }
                else if (usuarioLogado.Tipo == TipoUsuario.CO)
                {
                    Console.WriteLine("Escolha uma opção:");
                    Console.WriteLine("1 - Funções de Disciplina (Cadastrar, Buscar, Atualizar e Excluir)");
                    Console.WriteLine("2 - Vincular Software com a Disciplina");
                    Console.WriteLine("3 - Solicitar Alocação");
                    Console.WriteLine("4 - Histórico de Alocações");
                    Console.WriteLine("5 - Listar e Buscar Laboratórios");
                    Console.WriteLine("6 - Listar e Buscar Softwares");
                    Console.WriteLine("7 - Logout");
                    Console.WriteLine("0 - Sair");
                    Console.Write("\nOpção: ");

                    opcao = Console.ReadLine();

                    // Verifica a conversão do tipo
                    if (!int.TryParse(opcao, out int opcaoSelecionada))
                    {
                        Console.WriteLine("\nDigite uma opção válida!");
                        continue;
                    }

                    switch (opcaoSelecionada)
                    {
                        case 1:

                            break;
                        case 2:

                            break;
                        case 3:

                            break;
                        case 4:

                            break;
                        case 5:

                            break;
                        case 6:
                            mensagem = ResponsavelTiView.MenuBuscasSoftwareInterface(softwareService);
                            Console.WriteLine($"\n{mensagem}");
                            break;
                        case 7:
                            usuarioAutenticado = false;
                            Console.WriteLine("\nLogout realizado com sucesso!");
                            break;
                        case 0:
                            usuarioAutenticado = false;
                            sistemaAtivo = false;
                            Console.WriteLine("\nSistema encerrado.\n");
                            break;
                        default:
                            Console.WriteLine("\nInforme um usuário válido!");
                            break;
                    }
                }
                else if (usuarioLogado.Tipo == TipoUsuario.RT)
                {
                    Console.WriteLine("Escolha uma opção:");
                    Console.WriteLine("1 - Funções de Software (Cadastrar, Buscar, Atualizar e Excluir)");
                    Console.WriteLine("2 - Listar e Buscar Laboratórios");
                    Console.WriteLine("3 - Listar e Buscar Disciplinas");
                    Console.WriteLine("4 - Logout");
                    Console.WriteLine("0 - Sair");
                    Console.Write("\nOpção: ");

                    opcao = Console.ReadLine();

                    // Verifica a conversão do tipo
                    if (!int.TryParse(opcao, out int opcaoSelecionada))
                    {
                        Console.WriteLine("\nDigite uma opção válida!");
                        continue;
                    }

                    switch (opcaoSelecionada)
                    {
                        case 1:
                            mensagem = ResponsavelTiView.CrudSoftwaresInterface(usuarioLogado, softwareService);
                            Console.WriteLine($"\n{mensagem}");
                            break;
                        case 2:

                            break;
                        case 3:

                            break;
                        case 4:
                            usuarioAutenticado = false;
                            Console.WriteLine("\nLogout realizado com sucesso!");
                            break;
                        case 0:
                            usuarioAutenticado = false;
                            sistemaAtivo = false;
                            Console.WriteLine("\nSistema encerrado.\n");
                            break;
                        default:
                            Console.WriteLine("\nInforme um usuário válido!");
                            break;
                    }
                }
            }
        }
    }
}