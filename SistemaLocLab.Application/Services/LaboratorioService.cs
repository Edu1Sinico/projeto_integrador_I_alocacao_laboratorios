using SistemaLocLab.Application.DTOs;
using SistemaLocLab.Application.Interfaces;
using SistemaLocLab.Domain.Entities;

namespace SistemaLocLab.Application.Services
{
    public class LaboratorioService : ILaboratorioService
    {
        private readonly ILaboratorioRepository _laboratorioRepository;

        public LaboratorioService(ILaboratorioRepository laboratorioRepository)
        {
            _laboratorioRepository = laboratorioRepository;
        }

        public async Task<List<LaboratorioDTO>> ObterLaboratorios()
        {
            var laboratorios = await _laboratorioRepository.ObterTodosAsync();

            return laboratorios.Select(MapearParaDTO).ToList();
        }

        public async Task<LaboratorioDTO?> ObterLaboratorioId(Guid id)
        {
            var laboratorio = await _laboratorioRepository.ObterPorIdAsync(id);

            if (laboratorio == null)
                return null;

            return MapearParaDTO(laboratorio);
        }

        public async Task<LaboratorioDTO?> ObterLaboratorioNumero(int numero)
        {
            var laboratorio = await _laboratorioRepository.ObterPorNumeroAsync(numero);

            if (laboratorio == null)
                return null;

            return MapearParaDTO(laboratorio);
        }

        public async Task<LaboratorioDTO> CriarLaboratorio(CreateLaboratorioDTO dto)
        {
            var laboratorio = new Laboratorios(
                dto.NumeroLaboratorio,
                dto.Bloco,
                dto.QtdeComputador);

            await _laboratorioRepository.AdicionarAsync(laboratorio);

            return MapearParaDTO(laboratorio);
        }

        public async Task<LaboratorioDTO?> AtualizarLaboratorio(Guid id, UpdateLaboratorioDTO dto)
        {
            var laboratorio = await _laboratorioRepository.ObterPorIdAsync(id);

            if (laboratorio == null)
                return null;

            laboratorio.Atualizar(
                dto.NumeroLaboratorio,
                dto.Bloco,
                dto.QtdeComputador);

            await _laboratorioRepository.AtualizarAsync(laboratorio);

            return MapearParaDTO(laboratorio);
        }

        public async Task<bool> RemoverLaboratorio(Guid id)
        {
            var laboratorio = await _laboratorioRepository.ObterPorIdAsync(id);

            if (laboratorio == null)
                return false;

            await _laboratorioRepository.RemoverAsync(id);

            return true;
        }

        private LaboratorioDTO MapearParaDTO(Laboratorios laboratorio)
        {
            return new LaboratorioDTO
            {
                IDLaboratorio = laboratorio.IDLaboratorio,
                NumeroLaboratorio = laboratorio.NumeroLaboratorio,
                Bloco = laboratorio.Bloco,
                QtdeComputador = laboratorio.QtdeComputador,
                CapacidadeMaxAluno = laboratorio.CapacidadeMaxAluno
            };
        }
    }
}
