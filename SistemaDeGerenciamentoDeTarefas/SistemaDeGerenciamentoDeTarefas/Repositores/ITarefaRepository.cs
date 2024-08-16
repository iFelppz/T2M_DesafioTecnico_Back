using SistemaDeGerenciamentoDeTarefas.Models;

namespace SistemaDeGerenciamentoDeTarefas.Repositores
{
    public interface ITarefaRepository
    {
        TarefaModel BuscarPorId(int id);
        IEnumerable<TarefaModel> GetAll();
        TarefaModel Adicionar(TarefaModel tarefaModel);
        void Atualizar(TarefaModel tarefaModel);
        void Deletar(int id);

        IEnumerable<TarefaModel> BuscarPorUsuarioId(int usuarioId);

    }
}
