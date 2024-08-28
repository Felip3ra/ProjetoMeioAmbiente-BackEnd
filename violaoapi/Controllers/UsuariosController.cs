using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using violaoapi.DTOs.Usuario;
using violaoapi.Models;
using violaoapi.Services.Interfaces;

namespace violaoapi.Controllers
{
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            if (usuarios == null)
            {
                return NotFound("Usuarios nao encontrados");
            }
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuario nao encontrado");
            }
            return Ok(usuario);
        }

        [HttpGet("email/{email}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> GetUsuarioByEmail(string email)
        {
            var usuario = await _usuarioService.GetUsuarioByEmailAsync(email);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost("CriarUsuarioTeste")]
        public async Task<IActionResult> CreateTestUser()
        {
            var usuarioDto = new UsuarioCreateDTO
            {
                Nome = "UsuarioTeste",
                Email = "testuser@example.com",
                Senha = BCrypt.Net.BCrypt.HashPassword("testpassword")
            };
            Console.WriteLine($"Hash gerado: {usuarioDto.Senha}");
            await _usuarioService.AddUsuarioAsync(usuarioDto);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CriarUsuario(UsuarioCreateDTO usuarioDto)
        {
            await _usuarioService.AddUsuarioAsync(usuarioDto);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioDto.Email }, usuarioDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> EditarUsuario(int id, UsuarioUpdateDTO usuarioDto)
        {
            await _usuarioService.UpdateUsuarioAsync(id, usuarioDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletarUsuario(int id)
        {
            await _usuarioService.DeleteUsuarioAsync(id);
            return NoContent();
        }
    }
}