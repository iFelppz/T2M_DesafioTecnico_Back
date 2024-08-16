using Dapper;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using SistemaDeGerenciamentoDeTarefas.DTO;
using SistemaDeGerenciamentoDeTarefas.Models;
using SistemaDeGerenciamentoDeTarefas.Repositores;
using System.Data;

namespace SistemaDeGerenciamentoDeTarefas.Service
{
  

    public class UsuarioService 
    {
        private readonly IPasswordHasher<UsuarioModel> _hashSenha;
        private readonly UsuarioRepository _usuarioDados;


        public UsuarioService(IPasswordHasher<UsuarioModel> hashSenha, UsuarioRepository usuarioDados)
        {
            _hashSenha = hashSenha;
            _usuarioDados = usuarioDados;

        }

        public async Task EmailExisteAsync(string email)
        {
            var usuario = _usuarioDados.FindByEmail(email);
            if (usuario != null)
            {
                throw new ArgumentException("O email já está sendo usado.");
            }
        }

        public async Task CadastrarUsuarioAsync(UsuarioInserirDTO usuarioDto)
        {
            if (usuarioDto.Senha != usuarioDto.ConfirmaSenha)
            {
                throw new ArgumentException("A senha e a confirmação da senha não coincidem.");
            }
            var UsuarioExiste =  _usuarioDados.FindByEmail(usuarioDto.Email);
            if(UsuarioExiste != null)
            {
                throw new ArgumentException("O email já esta sendo usado.");
            }
            
            var senhaCriptografada = _hashSenha.HashPassword(null, usuarioDto.Senha);
            usuarioDto.Senha = senhaCriptografada;

            _usuarioDados.CadastrarUsuarioAsync(usuarioDto);
        }

        public async Task<UsuarioModel> GetAsync(string email, string senha)
        {
         
            var usuario = await _usuarioDados.FindByEmail(email);

           
            if (usuario == null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

         
            var resultadoVerificacao = _hashSenha.VerifyHashedPassword(usuario, usuario.Senha, senha);

            if (resultadoVerificacao == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Senha incorreta.");
            }

            return usuario;
        }
    }
}