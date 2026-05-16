using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Infrastructure.Repositories.Interfaces
{
    public interface ISoftwareRepository
    {
        Task AdicionarAsync(Software software);

        Task AtualizarAsync(Software software);

        Task RemoverAsync(Guid id);

        Task<Software?> ObterPorIdAsync(Guid id);

        Task<IEnumerable<Software>> ObterTodosAsync();
    }
}