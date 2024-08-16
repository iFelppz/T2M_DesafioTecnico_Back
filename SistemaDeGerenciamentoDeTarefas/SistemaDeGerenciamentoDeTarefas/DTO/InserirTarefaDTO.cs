using SistemaDeGerenciamentoDeTarefas.Enums;

namespace SistemaDeGerenciamentoDeTarefas.DTO
{
    public class InserirTarefaDTO
    {
        public string Titulo {  get; set; }

        public string Descricao { get; set; }

        public StatusTarefa Status { get; set; }

        public int UsuarioId { get; set; }

        public DateTime Prazo { get; set; }

    }
}
