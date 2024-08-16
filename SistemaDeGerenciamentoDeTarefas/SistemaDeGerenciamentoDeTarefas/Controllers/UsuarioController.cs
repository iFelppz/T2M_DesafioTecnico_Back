using Microsoft.AspNetCore.Mvc;
using SistemaDeGerenciamentoDeTarefas.Config;
using SistemaDeGerenciamentoDeTarefas.DTO;
using SistemaDeGerenciamentoDeTarefas.Models;
using SistemaDeGerenciamentoDeTarefas.Repositores;
using SistemaDeGerenciamentoDeTarefas.Service;

namespace SistemaDeGerenciamentoDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;


        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;

        }

        [HttpGet("{email}")]
        public async Task<IActionResult> ObterPorEmail(string email)
        {
            var usuario = _usuarioService.EmailExisteAsync(email);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }


        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarUsuarioAsync(UsuarioInserirDTO usuarioDto)
        {
            try
            {
                await _usuarioService.CadastrarUsuarioAsync(usuarioDto);
                return Ok("Usuário cadastrado com sucesso.");
            }
            catch (ArgumentException ex) when (ex.Message.Contains("O email já está sendo usado"))
            {
                return BadRequest(new { message = "O email já está sendo usado." });
            }
            catch (Exception ex)
            {
                // Log de erro genérico, se necessário
                return StatusCode(500, new { message = "Ocorreu um erro ao cadastrar o usuário." });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UsuarioLoginDTO usuarioDTOLogin)
        {
            try
            {
                // Recupera o usuário de forma assíncrona usando o DTO
                var user = await _usuarioService.GetAsync(usuarioDTOLogin.Email, usuarioDTOLogin.Senha);

                // Verifica se o usuário existe
                if (user == null)
                {
                    return NotFound(new { message = "Usuário ou senha inválidos" });
                }

                // Gera o Token
                var token = TokenService.GenerateToken(user);

                // Oculta a senha
                user.Senha = "";

                // Retorna os dados
                return Ok(new
                {
                    user = user,
                    token = token
                });
            }
            catch (ArgumentException ex)
            {
                // Retorna erro caso o usuário ou senha estejam incorretos
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Retorna erro genérico em caso de exceções inesperadas
                return StatusCode(500, new { message = "Ocorreu um erro ao autenticar o usuário." });
            }
        }

        [HttpGet("recuperarUsuario")]
        public async Task<IActionResult> RecuperarUsuarioAsync(string email, string senha)
        {
            try
            {
                var usuario = await _usuarioService.GetAsync(email, senha);
                return Ok(usuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log de erro genérico, se necessário
                return StatusCode(500, new { message = "Ocorreu um erro ao recuperar o usuário." });
            }
        }


    }
}
