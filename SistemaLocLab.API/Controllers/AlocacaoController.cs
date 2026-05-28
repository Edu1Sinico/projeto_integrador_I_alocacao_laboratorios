using Microsoft.AspNetCore.Mvc;

namespace SistemaLocLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlocacaoController : ControllerBase
    {
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(new
            {
                mensagem = "Lista de alocações"
            });
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            return Ok(new
            {
                id,
                mensagem = "Alocação encontrada"
            });
        }

        [HttpPost]
        public IActionResult Criar()
        {
            return Created("", new
            {
                mensagem = "Alocação criada"
            });
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id)
        {
            return Ok(new
            {
                id,
                mensagem = "Alocação atualizada"
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            return Ok(new
            {
                id,
                mensagem = "Alocação removida"
            });
        }
    }
}