using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Application.Interfaces
{
    public interface ISoftwareRepository
    {
        Task AdicionarAsync(Software software);

        Task AtualizarAsync(Software software);

        Task RemoverAsync(Guid id);

        Task<Software?> ObterPorIdAsync(Guid id);

        Task<IEnumerable<Software>> ObterTodosAsync();
        Task<IEnumerable<Software>> BuscarPorNomeAsync(string nome);
    }
}