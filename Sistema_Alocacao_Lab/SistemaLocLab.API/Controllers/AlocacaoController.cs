using Microsoft.AspNetCore.Mvc;
using SistemaLocLab.Application.DTOs;
using SistemaLocLab.Application.Interfaces;

namespace SistemaLocLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlocacaoController : ControllerBase
    {
        private readonly IAlocacaoService _alocacaoService;

        public AlocacaoController(IAlocacaoService alocacaoService)
        {
            _alocacaoService = alocacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var alocacoes = await _alocacaoService.ObterAlocacoes();

            return Ok(alocacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var alocacao = await _alocacaoService.ObterAlocacaoId(id);

            if (alocacao == null)
                return NotFound("Alocacao nao encontrada.");

            return Ok(alocacao);
        }

        [HttpGet("laboratorio/{laboratorioId}")]
        public async Task<IActionResult> ObterPorLaboratorio(Guid laboratorioId)
        {
            var alocacoes = await _alocacaoService.ObterAlocacoesPorLaboratorio(laboratorioId);

            return Ok(alocacoes);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CreateAlocacaoDTO dto)
        {
            var alocacao = await _alocacaoService.CriarAlocacao(dto);

            if (alocacao == null)
                return BadRequest("Laboratorio, disciplina ou usuario nao encontrado.");

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = alocacao.IdAlocacao },
                alocacao);
        }

        [HttpPut("{id}/horario")]
        public async Task<IActionResult> AtualizarHorario(Guid id, [FromBody] UpdateAlocacaoDTO dto)
        {
            var alocacao = await _alocacaoService.AtualizarHorario(id, dto);

            if (alocacao == null)
                return NotFound("Alocacao nao encontrada.");

            return Ok(alocacao);
        }

        [HttpPatch("{id}/aprovar")]
        public async Task<IActionResult> Aprovar(Guid id)
        {
            var alocacao = await _alocacaoService.AprovarAlocacao(id);

            if (alocacao == null)
                return NotFound("Alocacao nao encontrada.");

            return Ok(alocacao);
        }

        [HttpPatch("{id}/reprovar")]
        public async Task<IActionResult> Reprovar(Guid id)
        {
            var alocacao = await _alocacaoService.ReprovarAlocacao(id);

            if (alocacao == null)
                return NotFound("Alocacao nao encontrada.");

            return Ok(alocacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var removido = await _alocacaoService.RemoverAlocacao(id);

            if (!removido)
                return NotFound("Alocacao nao encontrada.");

            return NoContent();
        }
    }
}
