using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Application.Interfaces
{
    public interface IAlocacaoRepository
    {
        Task AdicionarAsync(Alocacao alocacao);

        Task AtualizarAsync(Alocacao alocacao);

        Task<Alocacao?> ObterPorIdAsync(Guid id);

        Task<IEnumerable<Alocacao>> ObterTodosAsync();

        Task<IEnumerable<Alocacao>> ObterPorLaboratorioAsync(Guid laboratorioId);
    }
}