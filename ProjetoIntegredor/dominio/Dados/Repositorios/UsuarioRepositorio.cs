using ProjetoIntegredor.dominio.Entidades;
using ProjetoIntegredor.dominio.Repositorio;

namespace ProjetoIntegredor.dominio.Dados.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AplicationDBContext _context;

        public UsuarioRepositorio(AplicationDBContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task Criar(Usuario usuario)
        {
            //Preparo para adicionar o usuário no banco de dados
            await contexto.Usuarios.AddAsync(usuario);
            //commit das criações no banco de dados
            await contexto.SaveChangesAsync();
        }

        public async Task Alterar(Usuario usuario)
        {
            //Preparo para atualizar o usuário no banco de dados
           ContextBoundObject.Usuarios.update(usuario);
           //commit das alterações no banco de dados
            await contexto.SaveChangesAsync();
        }

        public async Task Excluir(Usuario usuario)
        {
            //Preparo para excluir o usuário no banco de dados
            contexto.Usuarios.Remove(usuario);
            //commit das exclusões no banco de dados
            await contexto.SaveChangesAsync();
        }

        public async Task<Usuario> ObterUsuario(int id)
        {
            var usuario = contexto.Database.
                                        FromSql($"SELECT * FROM public.Usuario WHERE IdUsuario = {id}");

            return await usuario.FirstOrDefaultAsync();
    
        }

        public async Task<List<Usuario>> ObterTodosOsUsuarios()
        {
            var usuario = contexto.Database.
                                        SqlQuery("SELECT * FROM public.Usuario");

            // Joins para realizar
            return await usuario.ToListAsync();
        }
        
    }
}