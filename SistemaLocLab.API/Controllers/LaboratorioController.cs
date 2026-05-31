using Microsoft.AspNetCore.Mvc;
using SistemaLocLab.Application.DTOs;
using SistemaLocLab.Application.Interfaces;

namespace SistemaLocLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratorioController : ControllerBase
    {
        private readonly ILaboratorioService _laboratorioService;

        public LaboratorioController(ILaboratorioService laboratorioService)
        {
            _laboratorioService = laboratorioService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var laboratorios = await _laboratorioService.ObterLaboratorios();

            return Ok(laboratorios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var laboratorio = await _laboratorioService.ObterLaboratorioId(id);

            if (laboratorio == null)
                return NotFound("Laboratorio nao encontrado.");

            return Ok(laboratorio);
        }

        [HttpGet("numero/{numero:int}")]
        public async Task<IActionResult> ObterPorNumero(int numero)
        {
            var laboratorio = await _laboratorioService.ObterLaboratorioNumero(numero);

            if (laboratorio == null)
                return NotFound("Laboratorio nao encontrado.");

            return Ok(laboratorio);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CreateLaboratorioDTO dto)
        {
            var laboratorio = await _laboratorioService.CriarLaboratorio(dto);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = laboratorio.IDLaboratorio },
                laboratorio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] UpdateLaboratorioDTO dto)
        {
            var laboratorio = await _laboratorioService.AtualizarLaboratorio(id, dto);

            if (laboratorio == null)
                return NotFound("Laboratorio nao encontrado.");

            return Ok(laboratorio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var removido = await _laboratorioService.RemoverLaboratorio(id);

            if (!removido)
                return NotFound("Laboratorio nao encontrado.");

            return NoContent();
        }
    }
}
