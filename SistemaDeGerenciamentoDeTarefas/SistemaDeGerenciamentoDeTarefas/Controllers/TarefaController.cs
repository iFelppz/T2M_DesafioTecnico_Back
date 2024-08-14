using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeGerenciamentoDeTarefas.DTO;
using SistemaDeGerenciamentoDeTarefas.Models;
using SistemaDeGerenciamentoDeTarefas.Service;

namespace SistemaDeGerenciamentoDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _tarefaService;

        public TarefaController(TarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TarefaModel>> GetTarefas()
        {
            return Ok(_tarefaService.ListarTudo());
        }

        [HttpGet("{id}")]
        public ActionResult<TarefaModel> GetTarefasPorId(int id)
        {
            var tarefa = _tarefaService.BuscarPorId(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpPost]
        public ActionResult CriarTarefa([FromBody] TarefaDTO taskDto)
        {
            _tarefaService.CriarTarefa(taskDto.Titulo, taskDto.Descricao);
            return CreatedAtAction(nameof(GetTarefasPorId), new { id = taskDto.Id }, taskDto);
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarTareafa(int id, [FromBody] TarefaDTO taskDto)
        {
            var tarefa = _tarefaService.BuscarPorId(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            _tarefaService.AtualizarTarefa(id, taskDto.Titulo, taskDto.Descricao, taskDto.Status);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var tarefa = _tarefaService.BuscarPorId(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            _tarefaService.DeletarTarefa(id);
            return NoContent();
        }
    }
}