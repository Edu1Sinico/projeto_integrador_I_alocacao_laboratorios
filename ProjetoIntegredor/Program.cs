using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.menu;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

internal class Program
{
    public static void Main(string[] args)
    {
        string? opcao = null; // Variável de controle

        UsuarioService usuarioService = new UsuarioService(); // Chama as funcionalidades do sistema
        usuarioService.CadastrarDiretor(); // Cadastra o diretor de forma temporária

        // Definindo as telas
        Usuario usuarioLogado = LoginView.LoginInterface(usuarioService);

        Console.WriteLine($"\nBem-vindo, {usuarioLogado.Nome}!");

        do
        {
            Console.WriteLine("\n====== PORTAL DE ALOCAÇÃO ======\n");

            if (usuarioLogado.Tipo == TipoUsuario.DI)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Cadastrar Usuários");
                Console.WriteLine("2 - Cadastrar Laboratório");
                Console.WriteLine("3 - Aprovar/Recusar Alocação");
                Console.WriteLine("4 - Histórico de Alocações");
                Console.WriteLine("5 - Listar Usuários"); // Dentro da classe "UsuarioView", dar opção do usuário para buscar usuário pelo nome, re ou listagem completa
                Console.WriteLine("5 - Listar Laboratórios"); // Dentro da classe "LaboratorioView", dar opção do usuário para buscar número e bloco do laboratório ou listagem completa
                Console.WriteLine("6 - Listar Disciplinas"); // Dentro da classe "DisciplinaView", dar opção do usuário para buscar disciplina pelo nome ou listagem completa
                Console.WriteLine("7 - Listar Softwares"); // Dentro da classe "SoftwareView", dar opção do usuário para buscar software pelo nome ou listagem completa
                Console.WriteLine("8 - Logout");
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

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    default:
                        Console.WriteLine("\nInforme um usuário válido!");
                        break;
                }
            }
            else if (usuarioLogado.Tipo == TipoUsuario.CO)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Cadastrar Disciplina");
                Console.WriteLine("2 - Solicitar Alocação");
                Console.WriteLine("3 - Histórico de Alocações");
                Console.WriteLine("4 - Listar Laboratórios");
                Console.WriteLine("5 - Listar Disciplinas");
                Console.WriteLine("6 - Listar Softwares");
                Console.WriteLine("7 - Logout");
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

                        break;
                    case 7:

                        break;
                    default:
                        Console.WriteLine("\nInforme um usuário válido!");
                        break;
                }
            }
            else if (usuarioLogado.Tipo == TipoUsuario.RT)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Cadastrar Software");
                Console.WriteLine("2 - Listar Laboratórios");
                Console.WriteLine("3 - Listar Disciplinas");
                Console.WriteLine("4 - Listar Softwares");
                Console.WriteLine("5 - Logout");
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
                    default:
                        Console.WriteLine("\nInforme um usuário válido!");
                        break;
                }
            }
        } while (usuarioLogado != null);
    }
}