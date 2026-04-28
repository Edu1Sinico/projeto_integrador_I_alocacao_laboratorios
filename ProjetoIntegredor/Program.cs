
using ProjetoIntegredor.dominio.enums;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

internal class Program
{
    public static void Main(string[] args)
    {

        // Funções do Service
        UsuarioService user = new UsuarioService();

        // Cadastrando o diretor
        Usuario diretor = user.CadastrarUsuario(
            "123",
            "Marcos",
            "123456",
            "marcosdiretor@einstein.com.br",
            TipoUsuario.DI
        );

        // Outros atributos
        int? opcao = null;

        // Login Usuario
        string? re = null, senha = null, opcaoUser = null;
        bool tipo = false;
        TipoUsuario tipoUsuario;

        do
        {
            // Login
            Console.WriteLine("\n====== LOGIN ======\n");
            // Tipo de Usuário
            Console.Write("Selecione o usuário:"
                + "\n1 - Diretor"
                + "\n2 - coordenador"
                + "\n3 - Responsável do TI");
            opcaoUser = Console.ReadLine();

            switch (Int32.Parse(opcaoUser))
            {
                case 1:
                    tipoUsuario = TipoUsuario.DI;
                    tipo = true;
                    break;
                case 2:
                    tipoUsuario = TipoUsuario.CO;
                    tipo = true;
                    break;
                case 3:
                    tipoUsuario = TipoUsuario.RT;
                    tipo = true;
                    break;
                default:
                    Console.WriteLine("Informe um dos usuários!");
                    tipo = false;
                    break;
            }

            Console.WriteLine("Informe o RE Institucional: ");
            re = Console.ReadLine();

            Console.WriteLine("Informe a senha: ");
            senha = Console.ReadLine();

            user.Login(tipoUsuario, re, senha);

        } while (!tipo);
    }
}