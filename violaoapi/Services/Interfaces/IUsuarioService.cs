using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using violaoapi.DTOs.Usuario;
using violaoapi.Models;

namespace violaoapi.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetUsuariosAsync();
        Task<UsuarioDTO> GetUsuarioByIdAsync(int id);
        Task<Usuario> GetUsuarioByEmailAsync(string email);
        Task AddUsuarioAsync(UsuarioCreateDTO usuario);
        Task UpdateUsuarioAsync(int id, UsuarioUpdateDTO usuario);
        Task DeleteUsuarioAsync(int id);
    }
}