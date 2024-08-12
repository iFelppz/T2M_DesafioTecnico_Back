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

        public int Id { get; private set; }

        public string Titulo { get; private set; }

        public string Descricao { get; private set; }

        public StatusTarefa Status { get; private set; }

        public DateTime DataCriacao { get; private set; }
        public DateTime DataAtualizacao { get; private set; }


    }

}
