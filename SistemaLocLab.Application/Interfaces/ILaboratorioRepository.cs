using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Application.Interfaces
{
    public interface ILaboratorioRepository
    {
        Task AdicionarAsync(Laboratorios laboratorio);

        Task AtualizarAsync(Laboratorios laboratorio);

        Task RemoverAsync(Guid id);

        Task<Laboratorios?> ObterPorIdAsync(Guid id);

        Task<Laboratorios?> ObterPorNumeroAsync(int numero);

        Task<IEnumerable<Laboratorios>> ObterTodosAsync();
    }
}