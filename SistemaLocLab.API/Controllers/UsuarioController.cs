using Microsoft.AspNetCore.Mvc;
using SistemaLocLab.Application.DTOs;
using SistemaLocLab.Application.Interfaces;

namespace SistemaLocLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var usuarios = await _usuarioService.ObterUsuarios();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var usuario = await _usuarioService.ObterUsuarioId(id);

            if (usuario == null)
                return NotFound("Usuario nao encontrado.");

            return Ok(usuario);
        }

        [HttpGet("email")]
        public async Task<IActionResult> ObterPorEmail([FromQuery] string email)
        {
            var usuario = await _usuarioService.ObterUsuarioEmail(email);

            if (usuario == null)
                return NotFound("Usuario nao encontrado.");

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CreateUsuarioDTO dto)
        {
            var usuario = await _usuarioService.CriarUsuario(dto);

            if (usuario == null)
                return Conflict("Email ja cadastrado.");

            return CreatedAtAction(
                nameof(ObterPorId),
                new { id = usuario.ID },
                usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var usuario = await _usuarioService.Login(dto);

            if (usuario == null)
                return Unauthorized("Email ou senha invalido.");

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] UpdateUsuarioDTO dto)
        {
            var usuario = await _usuarioService.AtualizarUsuario(id, dto);

            if (usuario == null)
                return NotFound("Usuario nao encontrado.");

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var removido = await _usuarioService.RemoverUsuario(id);

            if (!removido)
                return NotFound("Usuario nao encontrado.");

            return NoContent();
        }
    }
}
