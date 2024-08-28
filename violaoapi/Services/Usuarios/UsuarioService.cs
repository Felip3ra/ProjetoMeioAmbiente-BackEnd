using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using violaoapi.DTOs.Usuario;
using violaoapi.Models;
using violaoapi.Repositories;
using violaoapi.Services.Interfaces;

namespace violaoapi.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetUsuariosAsync();
            var usuariosDTOs = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            });
            return usuariosDTOs;
        }

        public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return null;
            }

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public async Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(email);
            return usuario;
        }

        public async Task AddUsuarioAsync(UsuarioCreateDTO usuarioDto)
        {
            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha)
                // Senha = usuarioDto.Senha
            };

            await _usuarioRepository.AddUsuarioAsync(usuario);
        }

        public async Task UpdateUsuarioAsync(int id, UsuarioUpdateDTO usuarioDto)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario != null)
            {
                usuario.Nome = usuarioDto.Nome;
                usuario.Email = usuarioDto.Email;
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha);

                await _usuarioRepository.UpdateUsuarioAsync(usuario);
            }
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            await _usuarioRepository.DeleteUsuarioAsync(id);
        }
    }
}