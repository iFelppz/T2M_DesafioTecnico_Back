using SistemaDeGerenciamentoDeTarefas.Enums;
using System;

namespace SistemaDeGerenciamentoDeTarefas.Models
{
    public class TarefaModel
    {
        private TarefaModel() { }
        public TarefaModel(string titulo, string descricao)
        {      
            Titulo = titulo;
            Descricao = descricao;
            Status = StatusTarefa.Pendente;
            DataCriacao = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
        }
        public void Atualizacao(string titulo, string descricao, StatusTarefa status)
        {
            Titulo = titulo;
            Descricao = descricao;
            Status = status;
            DataAtualizacao = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Titulo { get;  set; }

        public string Descricao { get; set; }

        public StatusTarefa Status { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }


    }

}
