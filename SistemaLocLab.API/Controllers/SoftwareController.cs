using Microsoft.AspNetCore.Mvc;

namespace SistemaLocLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareController : ControllerBase
    {
        // GET api/software
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(new
            {
                mensagem = "Lista de softwares"
            });
        }

        // GET api/software/{id}
        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            return Ok(new
            {
                id = id,
                mensagem = "Software encontrado"
            });
        }

        // POST api/software
        [HttpPost]
        public IActionResult Criar()
        {
            return Created("", new
            {
                mensagem = "Software criado"
            });
        }

        // PUT api/software/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id)
        {
            return Ok(new
            {
                id = id,
                mensagem = "Software atualizado"
            });
        }

        // DELETE api/software/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(Guid id)
        {
            return Ok(new
            {
                id = id,
                mensagem = "Software removido"
            });
        }
    }
}