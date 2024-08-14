using SistemaDeGerenciamentoDeTarefas.Enums;

namespace SistemaDeGerenciamentoDeTarefas.DTO
{
    public class TarefaDTO
    { 
        public TarefaDTO(int id, string titulo, string descricao, StatusTarefa status)
        {
            Id = id;
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
            return obj is TarefaDTO dTO &&
                   Id == dTO.Id &&
                   Titulo == dTO.Titulo &&
                   Descricao == dTO.Descricao &&
                   Status == dTO.Status;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
