using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace violaoapi.DTOs.Usuario.ResetSenha
{
    public class ForgotPasswordDTO
    {
        public string Email { get; set; } = string.Empty;
    }
    public class ResetPassWordDTO
    {
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}