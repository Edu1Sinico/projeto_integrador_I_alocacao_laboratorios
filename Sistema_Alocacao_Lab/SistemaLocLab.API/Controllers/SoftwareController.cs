using Microsoft.AspNetCore.Mvc;
using SistemaLocLab.Application.DTOs;
using SistemaLocLab.Application.Interfaces;

namespace SistemaLocLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareController : ControllerBase
    {
        private readonly ISoftwareService _softwareService;

        public SoftwareController(ISoftwareService softwareService)
        {
            _softwareService = softwareService;
        }

        // =========================================
        // GET api/software
        // Lista todos os softwares
        // =========================================
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var softwares = await _softwareService.ObterSoftwares();

            return Ok(softwares);
        }

        // =========================================
        // GET api/software/{id}
        // Busca software por ID
        // =========================================
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var software = await _softwareService.ObterSoftwareId(id);

            if (software == null)
                return NotFound("Software não encontrado.");

            return Ok(software);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorNome([FromQuery] string nome)
        {
            var softwares = await _softwareService.BuscarSoftwaresNome(nome);

            return Ok(softwares);
        }

        // =========================================
        // POST api/software
        // Cria um novo software
        // =========================================
        [HttpPost]
        public async Task<IActionResult> Criar(
      [FromBody] CreateSoftwareDTO dto)
        {
            var software =
                await _softwareService
                    .CriarSoftware(dto);

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = software.IdSoftware },
                software);
        }

        // =========================================
        // PUT api/software/{id}
        // Atualiza um software
        // =========================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            Guid id,
            [FromBody] UpdateSoftwareDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var software = await _softwareService.AtualizarSoftware(id, dto);

            if (software == null)
                return NotFound("Software não encontrado.");

            return Ok(software);
        }

        // =========================================
        // DELETE api/software/{id}
        // Remove software
        // =========================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(
        Guid id)
        {
            bool removido =
                await _softwareService
                    .RemoverSoftware(id);

            if (!removido)
                return NotFound();

            return NoContent();
        }
    }
}
