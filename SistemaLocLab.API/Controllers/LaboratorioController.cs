using Microsoft.AspNetCore.Mvc;

namespace SistemaLocLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratorioController : ControllerBase
    {
        // GET api/laboratorio
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(new
            {
                mensagem = "Lista de laboratórios"
            });
        }

        // GET api/laboratorio/{id}
        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            return Ok(new
            {
                id = id,
                mensagem = "Laboratório encontrado"
            });
        }

        // POST api/laboratorio
        [HttpPost]
        public IActionResult Criar()
        {
            return Created("", new
            {
                mensagem = "Laboratório criado"
            });
        }

        // PUT api/laboratorio/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id)
        {
            return Ok(new
            {
                id = id,
                mensagem = "Laboratório atualizado"
            });
        }

        // DELETE api/laboratorio/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            return Ok(new
            {
                id = id,
                mensagem = "Laboratório removido"
            });
        }
    }
}