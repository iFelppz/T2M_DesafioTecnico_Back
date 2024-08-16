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
        private readonly RabbitMqService _rabbitMqService;

        public TarefaController(TarefaService tarefaService, RabbitMqService rabbitMqService)
        {
            _tarefaService = tarefaService;
            _rabbitMqService = rabbitMqService;
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
            _tarefaService.CriarTarefa(taskDto);
         
            var message = $"Tarefa criada: {taskDto.Titulo} - {taskDto.Descricao}";
            _rabbitMqService.PublishMessage(message);
          
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

            var message = $"Tarefa atualizada: {taskDto.Titulo} - {taskDto.Descricao} - Status: {taskDto.Status}";
            _rabbitMqService.PublishMessage(message);

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

            var message = $"Tarefa deletada: {tarefa.Titulo} - {tarefa.Descricao}";
            _rabbitMqService.PublishMessage(message);

            return NoContent();
        }

        [HttpGet("usuario/{usuarioId}")]
        public ActionResult<IEnumerable<TarefaModel>> GetTarefasPorUsuario(int usuarioId)
        {
            var tarefas = _tarefaService.BuscarTarefasPorUsuario(usuarioId);
            if (tarefas == null || !tarefas.Any())
            {
                return NotFound();
            }
            return Ok(tarefas);
        }
    }
}