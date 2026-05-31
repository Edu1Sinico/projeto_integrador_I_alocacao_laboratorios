using Microsoft.AspNetCore.Mvc;
using SistemaLocLab.Application.DTOs;
using SistemaLocLab.Application.Interfaces;

namespace SistemaLocLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly IDisciplinaService _disciplinaService;

        public DisciplinaController(IDisciplinaService disciplinaService)
        {
            _disciplinaService = disciplinaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var disciplinas = await _disciplinaService.ObterDisciplinas();

            return Ok(disciplinas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var disciplina = await _disciplinaService.ObterDisciplinaID(id);

            if (disciplina == null)
                return NotFound("Disciplina nao encontrada.");

            return Ok(disciplina);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorNome([FromQuery] string nome)
        {
            var disciplinas = await _disciplinaService.BuscarDisciplinasNome(nome);

            return Ok(disciplinas);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CreateDisciplinaDTO dto)
        {
            var disciplina = await _disciplinaService.CriarDisciplina(dto);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = disciplina.IdDisciplina },
                disciplina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] UpdateDisciplinaDTO dto)
        {
            var disciplina = await _disciplinaService.AtualizarDisciplina(id, dto);

            if (disciplina == null)
                return NotFound("Disciplina nao encontrada.");

            return Ok(disciplina);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var removido = await _disciplinaService.RemoverDisciplina(id);

            if (!removido)
                return NotFound("Disciplina nao encontrada.");

            return NoContent();
        }
    }
}
