using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace violaoapi.DTOs.Usuario
{
    public class UsuarioUpdateDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}