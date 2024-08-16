using Dapper;
using Npgsql;
using SistemaDeGerenciamentoDeTarefas.DTO;
using SistemaDeGerenciamentoDeTarefas.Models;
using System.Data;

namespace SistemaDeGerenciamentoDeTarefas.Repositores
{
    public class UsuarioRepositoryDados : UsuarioRepository
    {
        private readonly IConfiguration _config;

        public UsuarioRepositoryDados(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection Connection => new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));

        
        protected IDbConnection GetConnectionForTesting() => Connection;

        public async Task<UsuarioModel> FindByEmail(string email)
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT * FROM Usuario WHERE email = @Email";
                return await dbConnection.QueryFirstOrDefaultAsync<UsuarioModel>(query, new { Email = email });
            }
        }

        public async Task CadastrarUsuarioAsync(UsuarioInserirDTO usuarioDto)
        {
            using (var dbConnection = Connection)
            {
                var query = "INSERT INTO Usuario (nome, email, senha) VALUES (@NomeDeUsuario, @Email, @SenhaCriptografada)";
                var parametros = new { NomeDeUsuario = usuarioDto.Nome, usuarioDto.Email, SenhaCriptografada = usuarioDto.Senha };

                 await dbConnection.ExecuteAsync(query, parametros);
            }
        }

    }



}
