using SistemaLocLab.Application.DTOs;

namespace SistemaLocLab.Application.Interfaces
{
    public interface IAlocacaoService
    {
        Task<List<AlocacaoDTO>> ObterAlocacoes();
        Task<AlocacaoDTO?> ObterAlocacaoId(Guid id);
        Task<List<AlocacaoDTO>> ObterAlocacoesPorLaboratorio(Guid laboratorioId);
        Task<AlocacaoDTO?> CriarAlocacao(CreateAlocacaoDTO dto);
        Task<AlocacaoDTO?> AtualizarHorario(Guid id, UpdateAlocacaoDTO dto);
        Task<AlocacaoDTO?> AprovarAlocacao(Guid id);
        Task<AlocacaoDTO?> ReprovarAlocacao(Guid id);
        Task<bool> RemoverAlocacao(Guid id);
    }
}
