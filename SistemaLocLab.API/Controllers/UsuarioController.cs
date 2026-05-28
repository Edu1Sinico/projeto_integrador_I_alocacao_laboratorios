using Microsoft.AspNetCore.Mvc;
using SistemaLocLab.Application.Interfaces;

namespace SistemaLocLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        // GET api/usuario
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(new
            {
                mensagem = "Lista de usuários"
            });
        }

        // GET api/usuario/{id}
        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            return Ok(new
            {
                id = id,
                mensagem = "Usuário encontrado"
            });
        }

        // POST api/usuario
        [HttpPost]
        public IActionResult Criar()
        {
            return Created("", new
            {
                mensagem = "Usuário criado"
            });
        }

        // PUT api/usuario/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id)
        {
            return Ok(new
            {
                id = id,
                mensagem = "Usuário atualizado"
            });
        }

        // DELETE api/usuario/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            return Ok(new
            {
                id = id,
                mensagem = "Usuário removido"
            });
        }
    }
}