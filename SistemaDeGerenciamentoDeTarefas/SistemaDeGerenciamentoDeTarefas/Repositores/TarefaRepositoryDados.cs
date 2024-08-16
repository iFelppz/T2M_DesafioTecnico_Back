using Dapper;
using Npgsql;
using SistemaDeGerenciamentoDeTarefas.Models;
using System.Data;

namespace SistemaDeGerenciamentoDeTarefas.Repositores
{
    public class TarefaRepositoryDados : ITarefaRepository
    {
        private readonly IConfiguration _config;

        public TarefaRepositoryDados(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection Connection => new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));

        public TarefaModel BuscarPorId(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT tarefa_id AS Id, titulo AS Titulo, descricao AS Descricao, status AS Status, data_criacao AS DataCriacao, data_atualizacao AS DataAtualizacao FROM Tarefa WHERE tarefa_id = @Id";
                return dbConnection.QueryFirstOrDefault<TarefaModel>(query, new { Id = id });
            }
        }

        public IEnumerable<TarefaModel> GetAll()
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT tarefa_id AS Id, titulo AS Titulo, descricao AS Descricao, status AS Status, data_criacao AS DataCriacao, data_atualizacao AS DataAtualizacao, usuario_id AS UsuarioId FROM Tarefa";
                return dbConnection.Query<TarefaModel>(query);
            }
        }

        public TarefaModel Adicionar(TarefaModel tarefaModel)
        {
            using (var dbConnection = Connection)
            {
                const string query = @"
                    INSERT INTO Tarefa (titulo, descricao, data_criacao, data_atualizacao)
                    VALUES (@Titulo, @Descricao, @DataCriacao, @DataAtualizacao)";
                dbConnection.Execute(query, tarefaModel);
            }
            return tarefaModel;
        }

        public void Atualizar(TarefaModel tarefaModel)
        {
            using (var dbConnection = Connection)
            {
                const string query = @"
                    UPDATE Tarefa
                    SET titulo = @Titulo, descricao = @Descricao, status = @Status, data_atualizacao = @DataAtualizacao
                    WHERE tarefa_id = @Id";
                dbConnection.Execute(query, tarefaModel);
            }
        }

        public void Deletar(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "DELETE FROM Tarefa WHERE tarefa_id = @Id";
                dbConnection.Execute(query, new { Id = id });
            }
        }

        public IEnumerable<TarefaModel> BuscarPorUsuarioId(int usuarioId)
        {
            using (var dbConnection = Connection)
            {
                const string query = @"
                SELECT tarefa_id AS Id, titulo AS Titulo, descricao AS Descricao, status AS Status, data_criacao AS DataCriacao, data_atualizacao AS DataAtualizacao, usuario_id AS UsuarioId
                FROM Tarefa
                WHERE usuario_id = @UsuarioId";
                return dbConnection.Query<TarefaModel>(query, new { UsuarioId = usuarioId });
            }
        }

       
    }
}
