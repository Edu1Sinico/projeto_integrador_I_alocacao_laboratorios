namespace ProjetoIntegredor.dominio.Repositorio
{
    public interface IUsuarioRepositorio
    {
         Task Criar(Usuario usuario);
         Task Alterar(Usuario usuario);
         Task Excluir(Usuario usuario);

         Task<Usuario> ObterUsuario(int id);
            Task<List<Usuario>> ObterTodosOsUsuarios();
    }
}