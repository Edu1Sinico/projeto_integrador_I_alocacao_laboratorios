using SistemaLocLab.Application.DTOs;

namespace SistemaLocLab.Application.Interfaces
{
    public interface IDisciplinaService
    {
        Task<List<DisciplinaDTO>> ObterDisciplinas();
        Task<DisciplinaDTO?> ObterDisciplinaID(Guid id);
        Task<List<DisciplinaDTO>> BuscarDisciplinasNome(string nome);
        Task<DisciplinaDTO> CriarDisciplina(CreateDisciplinaDTO dto);
        Task<DisciplinaDTO?> AtualizarDisciplina(Guid id, UpdateDisciplinaDTO dto);
        Task<bool> RemoverDisciplina(Guid id);
    }
}
