using Microsoft.AspNetCore.Mvc;

namespace SistemaLocLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        // GET api/disciplina
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(new
            {
                mensagem = "Lista de disciplinas"
            });
        }

        // GET api/disciplina/{id}
        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            return Ok(new
            {
                id,
                mensagem = "Disciplina encontrada"
            });
        }

        // POST api/disciplina
        [HttpPost]
        public IActionResult Criar()
        {
            return Created("", new
            {
                mensagem = "Disciplina criada"
            });
        }

        // PUT api/disciplina/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id)
        {
            return Ok(new
            {
                id,
                mensagem = "Disciplina atualizada"
            });
        }

        // DELETE api/disciplina/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            return Ok(new
            {
                id,
                mensagem = "Disciplina removida"
            });
        }
    }
}