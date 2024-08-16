using SistemaDeGerenciamentoDeTarefas.Models;

namespace SistemaDeGerenciamentoDeTarefas.Repositores
{
    public interface ITarefaRepositoryDados
    {
        void Adicionar(TarefaModel tarefaModel);
    }
}