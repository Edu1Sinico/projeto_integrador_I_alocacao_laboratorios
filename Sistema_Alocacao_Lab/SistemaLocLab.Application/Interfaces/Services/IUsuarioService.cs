using SistemaLocLab.Application.DTOs;

namespace SistemaLocLab.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> ObterUsuarios();
        Task<UsuarioDTO?> ObterUsuarioId(Guid id);
        Task<UsuarioDTO?> ObterUsuarioEmail(string email);
        Task<UsuarioDTO?> Login(LoginDTO dto);
        Task<UsuarioDTO?> CriarUsuario(CreateUsuarioDTO dto);
        Task<UsuarioDTO?> AtualizarUsuario(Guid id, UpdateUsuarioDTO dto);
        Task<bool> RemoverUsuario(Guid id);
    }
}
