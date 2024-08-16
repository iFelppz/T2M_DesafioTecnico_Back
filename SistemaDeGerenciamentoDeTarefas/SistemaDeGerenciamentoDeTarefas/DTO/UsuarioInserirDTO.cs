using SistemaDeGerenciamentoDeTarefas.Models;
using System.Text.Json.Serialization;

namespace SistemaDeGerenciamentoDeTarefas.DTO
{
    public class UsuarioInserirDTO
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string ConfirmaSenha { get; set; }

        public UsuarioInserirDTO(UsuarioModel usuarioModel)
        {
            this.Nome = usuarioModel.Nome;
            this.Email = usuarioModel.Email;
            this.Senha = usuarioModel.Senha;
        }

        [JsonConstructor]
        public UsuarioInserirDTO (string nome, string email, string senha, string confirmasenha)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.ConfirmaSenha = confirmasenha;
        }

        public UsuarioInserirDTO() { }

    }
}
