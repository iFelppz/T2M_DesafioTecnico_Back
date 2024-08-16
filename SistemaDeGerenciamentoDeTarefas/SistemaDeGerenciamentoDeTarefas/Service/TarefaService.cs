using Microsoft.AspNetCore.Http.HttpResults;
using SistemaDeGerenciamentoDeTarefas.DTO;
using SistemaDeGerenciamentoDeTarefas.Enums;
using SistemaDeGerenciamentoDeTarefas.Models;
using SistemaDeGerenciamentoDeTarefas.Repositores;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeGerenciamentoDeTarefas.Service
{
    public class TarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }
        public TarefaDTO CriarTarefa(InserirTarefaDTO tarefaDto)
        {
            TarefaModel tarefaModel = new();
            tarefaModel.Titulo = tarefaDto.Titulo;
            tarefaModel.Descricao = tarefaDto.Descricao;
            tarefaModel.UsuarioId = tarefaDto.UsuarioId;
            tarefaModel.Status = tarefaDto.Status;
            tarefaModel.Prazo = tarefaDto.Prazo;

            tarefaModel = _tarefaRepository.Adicionar(tarefaModel);

            TarefaDTO tarefaDTO = new TarefaDTO(tarefaModel);
            return tarefaDTO;
        }

        public void AtualizarTarefa(int id, string titulo, string descricao, StatusTarefa status, DateTime prazo)
        {
            var tarefa = _tarefaRepository.BuscarPorId(id);
            if (tarefa != null)
            {
                tarefa.Atualizacao(titulo, descricao, status, prazo);
                _tarefaRepository.Atualizar(tarefa);
            }
        }
        public void DeletarTarefa(int id)
        {
            var tarefa = _tarefaRepository.BuscarPorId(id);
            if (tarefa != null)
            {
                _tarefaRepository.Deletar(id);
            }
        }

            public IEnumerable<TarefaModel> ListarTudo()
        {
            return _tarefaRepository.GetAll();
        }

        public TarefaModel BuscarPorId(int id)
        {
            return _tarefaRepository.BuscarPorId(id);
        }

        public IEnumerable<TarefaModel> BuscarTarefasPorUsuario(int usuarioId)
        {
            return _tarefaRepository.BuscarPorUsuarioId(usuarioId);
        }
    }
}
