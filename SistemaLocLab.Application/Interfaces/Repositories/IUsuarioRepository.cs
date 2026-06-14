using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AdicionarAsync(Usuarios usuario);

        Task AtualizarAsync(Usuarios usuario);

        Task RemoverAsync(Guid id);

        Task<Usuarios?> ObterPorIdAsync(Guid id);

        Task<Usuarios?> ObterPorEmailAsync(string email);

        Task<IEnumerable<Usuarios>> ObterTodosAsync();

        Task<bool> ExisteEmailAsync(string email);
    }
}