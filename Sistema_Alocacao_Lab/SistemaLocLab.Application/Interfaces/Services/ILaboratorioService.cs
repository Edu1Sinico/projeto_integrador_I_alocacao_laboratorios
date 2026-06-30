using SistemaLocLab.Application.DTOs;

namespace SistemaLocLab.Application.Interfaces
{
    public interface ILaboratorioService
    {
        Task<List<LaboratorioDTO>> ObterLaboratorios();
        Task<LaboratorioDTO?> ObterLaboratorioId(Guid id);
        Task<LaboratorioDTO?> ObterLaboratorioNumero(int numero);
        Task<LaboratorioDTO> CriarLaboratorio(CreateLaboratorioDTO dto);
        Task<LaboratorioDTO?> AtualizarLaboratorio(Guid id, UpdateLaboratorioDTO dto);
        Task<bool> RemoverLaboratorio(Guid id);
    }
}
