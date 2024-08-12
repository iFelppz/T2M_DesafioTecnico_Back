using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeGerenciamentoDeTarefas.Models;

namespace SistemaDeGerenciamentoDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<UsuarioModel>> ListarTodos()
        {
            return Ok();
        }
    }
}
