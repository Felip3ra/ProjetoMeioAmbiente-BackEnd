using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using violaoapi.Models.Login;
using violaoapi.Services.Auth;
using violaoapi.Services.Interfaces;

namespace violaoapi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUsuarioService usuarioService, AuthService authService, ILogger<AuthController> logger)
        {
            _usuarioService = usuarioService;
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            _logger.LogInformation($"Tentativa de login com o email: {login.Email}");

            // Verifica se o modelo de login é válido
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo de login inválido.");
                return BadRequest("Modelo de login inválido.");
            }

            // Obtenha o usuário pelo email
            var usuario = await _usuarioService.GetUsuarioByEmailAsync(login.Email);
            if (usuario == null)
            {
                _logger.LogWarning($"Login falhou: Usuário com o email {login.Email} não encontrado.");
                return Unauthorized("Usuaruio nao autorizado");
            }

            // Verifique se a senha fornecida corresponde à senha armazenada (hash)
            bool senhaCorreta = BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha);
            if (!senhaCorreta)
            {
                _logger.LogWarning($"Login falhou: Senha incorreta para o usuário com o email {login.Email}.");
                return Unauthorized("Senha invalida!");
            }

            // Gere o token JWT
            var token = _authService.GenerateToken(usuario);
            usuario.Senha = "";
            _logger.LogInformation($"Login bem-sucedido para o usuário com o email {login.Email}.");
            return Ok(new { Token = token, User = usuario });
        }
    }
}