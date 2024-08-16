using SistemaDeGerenciamentoDeTarefas.Enums;
using SistemaDeGerenciamentoDeTarefas.Models;

namespace SistemaDeGerenciamentoDeTarefas.DTO
{
    public class TarefaDTO
    {
        public TarefaDTO() { }

        public TarefaDTO(int id, string titulo, string descricao, StatusTarefa status, int usuarioId, DateTime prazo)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Status = status;
            UsuarioId = usuarioId;
            Prazo = prazo;
        }

        public TarefaDTO(TarefaModel tarefaModel)
        {
            this.Id = tarefaModel.Id;
            this.Titulo = tarefaModel.Titulo;
            this.Descricao = tarefaModel.Descricao;
            this.Status = tarefaModel.Status;
            this.UsuarioId = tarefaModel.UsuarioId;
            this.Prazo = tarefaModel.Prazo;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusTarefa Status { get; set; }

        public int UsuarioId { get; set; }

        public DateTime Prazo { get; set; }

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
