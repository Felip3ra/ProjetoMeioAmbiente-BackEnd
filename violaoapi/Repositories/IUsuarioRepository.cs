using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using violaoapi.Models;

namespace violaoapi.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> GetUsuarioByEmailAsync(string email);
        Task AddUsuarioAsync(Usuario usuario);
        Task UpdateUsuarioAsync(Usuario usuario);
        Task DeleteUsuarioAsync(int id);
    }
}