using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using violaoapi.Repositories;
using violaoapi.Services.Auth;
using violaoapi.Services.Email;
using violaoapi.Services.Interfaces;
using violaoapi.Services.Usuarios;

namespace violaoapi.Configurations
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositórios
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Serviços
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddSingleton<AuthService>();
            services.AddTransient<EmailService>();

            // Parte de Email
            services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
        }
    }
}