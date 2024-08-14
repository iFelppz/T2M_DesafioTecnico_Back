using SistemaDeGerenciamentoDeTarefas.Enums;
using SistemaDeGerenciamentoDeTarefas.Models;
using SistemaDeGerenciamentoDeTarefas.Repositores;
using System;
using System.Collections.Generic;

namespace SistemaDeGerenciamentoDeTarefas.Service
{
    public class TarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }
        public void CriarTarefa(string titulo, string descricao)
        {
            var tarefa = new TarefaModel(titulo, descricao);
            _tarefaRepository.Adicionar(tarefa);
        }
        public void AtualizarTarefa(int id, string titulo, string descricao, StatusTarefa status)
        {
            var tarefa = _tarefaRepository.BuscarPorId(id);
            if (tarefa != null)
            {
                tarefa.Atualizacao(titulo, descricao, status);
                _tarefaRepository.Atualizar(tarefa);
            }
        }
        public void DeletarTarefa(int id)
        {
            _tarefaRepository.Deletar(id);
        }

        public IEnumerable<TarefaModel> ListarTudo()
        {
            return _tarefaRepository.GetAll();
        }

        public TarefaModel BuscarPorId(int id)
        {
            return _tarefaRepository.BuscarPorId(id);
        }
    }
}
