using SistemaDeGerenciamentoDeTarefas.DTO;
using SistemaDeGerenciamentoDeTarefas.Models;

namespace SistemaDeGerenciamentoDeTarefas.Repositores
{
    public interface UsuarioRepository
    {
        Task<UsuarioModel> FindByEmail(string email);

        Task CadastrarUsuarioAsync(UsuarioInserirDTO usuarioDto);
    }
}
