using ProjetoIntegredor.menu;
using ProjetoIntegredor.model;
using ProjetoIntegredor.Servicos;

internal class Program
{
    public static void Main(string[] args)
    {
        UsuarioService usuarioService = new UsuarioService(); // Chama às funcionalidades do sistema
        usuarioService.CadastrarDiretor(); // Cadastra o diretor de forma temporária

        // Definindo às telas
        Usuario usuarioLogado = LoginView.LoginInterface(usuarioService);

        Console.WriteLine($"\nBem-vindo, {usuarioLogado.Nome}!");
    }
}