using SistemaDeGerenciamentoDeTarefas.Models;

namespace SistemaDeGerenciamentoDeTarefas.Repositores
{
    public interface ITarefaRepository
    {
        TarefaModel BuscarPorId(int id);
        IEnumerable<TarefaModel> GetAll();
        void Adicionar(TarefaModel tarefaModel);
        void Atualizar(TarefaModel tarefaModel);
        void Deletar(int id);

    }
}
