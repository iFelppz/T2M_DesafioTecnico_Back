using SistemaDeGerenciamentoDeTarefas.Enums;
using System;

namespace SistemaDeGerenciamentoDeTarefas.Models
{
    public class TarefaModel
    {
        public TarefaModel() { }
        public TarefaModel(string titulo, string descricao, int usuarioId, DateTime prazo)
        {      
            Titulo = titulo;
            Descricao = descricao;
            Status = StatusTarefa.Pendente;
            DataCriacao = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
            UsuarioId = usuarioId;
            Prazo = prazo;
        }
        public void Atualizacao(string titulo, string descricao, StatusTarefa status, DateTime prazo)
        {
            Titulo = titulo;
            Descricao = descricao;
            Status = status;
            DataAtualizacao = DateTime.UtcNow;
            Prazo = prazo;
        }

        public int Id { get; set; }

        public string Titulo { get;  set; }

        public string Descricao { get; set; }

        public StatusTarefa Status { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public int UsuarioId { get; set; }

        public DateTime Prazo { get; set; }
    }

}
