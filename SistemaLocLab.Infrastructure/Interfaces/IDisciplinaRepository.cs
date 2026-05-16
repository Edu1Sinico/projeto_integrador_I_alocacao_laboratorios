using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Infrastructure.Repositories.Interfaces
{
    public interface IDisciplinaRepository
    {
        Task AdicionarAsync(Disciplina disciplina);

        Task AtualizarAsync(Disciplina disciplina);

        Task RemoverAsync(Guid id);

        Task<Disciplina?> ObterPorIdAsync(Guid id);

        Task<IEnumerable<Disciplina>> ObterTodosAsync();
    }
}