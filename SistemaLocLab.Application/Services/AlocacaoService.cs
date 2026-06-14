using SistemaLocLab.Application.DTOs;
using SistemaLocLab.Application.Interfaces;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Application.Services
{
    public class AlocacaoService : IAlocacaoService
    {
        private readonly IAlocacaoRepository _alocacaoRepository;
        private readonly ILaboratorioRepository _laboratorioRepository;
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AlocacaoService(
            IAlocacaoRepository alocacaoRepository,
            ILaboratorioRepository laboratorioRepository,
            IDisciplinaRepository disciplinaRepository,
            IUsuarioRepository usuarioRepository)
        {
            _alocacaoRepository = alocacaoRepository;
            _laboratorioRepository = laboratorioRepository;
            _disciplinaRepository = disciplinaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<AlocacaoDTO>> ObterAlocacoes()
        {
            var alocacoes = await _alocacaoRepository.ObterTodosAsync();

            return alocacoes.Select(MapearParaDTO).ToList();
        }

        public async Task<AlocacaoDTO?> ObterAlocacaoId(Guid id)
        {
            var alocacao = await _alocacaoRepository.ObterPorIdAsync(id);

            if (alocacao == null)
                return null;

            return MapearParaDTO(alocacao);
        }

        public async Task<List<AlocacaoDTO>> ObterAlocacoesPorLaboratorio(Guid laboratorioId)
        {
            var alocacoes = await _alocacaoRepository.ObterPorLaboratorioAsync(laboratorioId);

            return alocacoes.Select(MapearParaDTO).ToList();
        }

        public async Task<AlocacaoDTO?> CriarAlocacao(CreateAlocacaoDTO dto)
        {
            var laboratorio = await _laboratorioRepository.ObterPorIdAsync(dto.LaboratorioId);
            var disciplina = await _disciplinaRepository.ObterPorIdAsync(dto.DisciplinaId);
            var usuario = await _usuarioRepository.ObterPorIdAsync(dto.UsuarioId);

            if (laboratorio == null || disciplina == null || usuario == null)
                return null;

            var alocacao = new Alocacao(
                dto.Data,
                dto.HoraInicio,
                dto.HoraFim,
                laboratorio,
                disciplina,
                usuario);

            await _alocacaoRepository.AdicionarAsync(alocacao);

            return MapearParaDTO(alocacao);
        }

        public async Task<AlocacaoDTO?> AtualizarHorario(Guid id, UpdateAlocacaoDTO dto)
        {
            var alocacao = await _alocacaoRepository.ObterPorIdAsync(id);

            if (alocacao == null)
                return null;

            alocacao.AtualizarHorario(dto.HoraInicio, dto.HoraFim);

            await _alocacaoRepository.AtualizarAsync(alocacao);

            return MapearParaDTO(alocacao);
        }

        public async Task<AlocacaoDTO?> AprovarAlocacao(Guid id)
        {
            var alocacao = await _alocacaoRepository.ObterPorIdAsync(id);

            if (alocacao == null)
                return null;

            alocacao.Aprovar();

            await _alocacaoRepository.AtualizarAsync(alocacao);

            return MapearParaDTO(alocacao);
        }

        public async Task<AlocacaoDTO?> ReprovarAlocacao(Guid id)
        {
            var alocacao = await _alocacaoRepository.ObterPorIdAsync(id);

            if (alocacao == null)
                return null;

            alocacao.Reprovar();

            await _alocacaoRepository.AtualizarAsync(alocacao);

            return MapearParaDTO(alocacao);
        }

        public async Task<bool> RemoverAlocacao(Guid id)
        {
            var alocacao = await _alocacaoRepository.ObterPorIdAsync(id);

            if (alocacao == null)
                return false;

            await _alocacaoRepository.RemoverAsync(id);

            return true;
        }

        private AlocacaoDTO MapearParaDTO(Alocacao alocacao)
        {
            return new AlocacaoDTO
            {
                IdAlocacao = alocacao.IdAlocacao,
                Data = alocacao.Data,
                HoraInicio = alocacao.HoraInicio,
                HoraFim = alocacao.HoraFim,
                Status = alocacao.Status,
                DataCriacao = alocacao.DataCriacao,
                LaboratorioId = alocacao.LaboratorioId,
                NumeroLaboratorio = alocacao.Laboratorio?.NumeroLaboratorio ?? 0,
                BlocoLaboratorio = alocacao.Laboratorio?.Bloco ?? string.Empty,
                DisciplinaId = alocacao.DisciplinaId,
                NomeDisciplina = alocacao.Disciplina?.NomeDisciplina ?? string.Empty,
                UsuarioId = alocacao.UsuarioId,
                NomeUsuario = alocacao.Usuario?.Nome ?? string.Empty
            };
        }
    }
}
