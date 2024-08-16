using SistemaDeGerenciamentoDeTarefas.Enums;

namespace SistemaDeGerenciamentoDeTarefas.DTO
{
    public class TarefaDTO
    {
        public TarefaDTO() { }

        public TarefaDTO(int id, string titulo, string descricao, StatusTarefa status)
        {
            Titulo = titulo;
            Descricao = descricao;
            Status = status;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusTarefa Status { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is TarefaDTO dTO)
            {
                return Id == dTO.Id &&
                       Titulo == dTO.Titulo &&
                       Descricao == dTO.Descricao &&
                       Status == dTO.Status;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Titulo, Descricao, Status);
        }
    }
}
