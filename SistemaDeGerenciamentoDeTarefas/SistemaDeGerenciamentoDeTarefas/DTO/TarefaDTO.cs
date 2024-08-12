using SistemaDeGerenciamentoDeTarefas.Enums;

namespace SistemaDeGerenciamentoDeTarefas.DTO
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusTarefa Status { get; set; }
    }
}
