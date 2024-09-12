using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace violaoapi.Services.Email
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendPasswordResetEmail(string toEmail, string token)
        {
            // Crie o link de redefinição de senha que será enviado para o usuário
            var resetLink = $"https://localhost:7203/api/reset-password?token={token}";
            var subject = "Recuperação de Senha";
            var body = $"Olá, <br><br>Para redefinir sua senha, clique no link abaixo:<br><a href='{resetLink}'>Redefinir Senha</a>";

            // Obtenha as configurações de SMTP do arquivo appsettings.json
            var smtpHost = _configuration["Smtp:Host"];
            var smtpPort = int.Parse(_configuration["Smtp:Port"]);
            var smtpUsername = _configuration["Smtp:Username"];
            var smtpPassword = _configuration["Smtp:Password"];
            var enableSSL = true;

            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = enableSSL;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true  // Permitir HTML no corpo do e-mail
                };
                mailMessage.To.Add(toEmail);  // Enviar para o e-mail do usuário

                try
                {
                    // Enviar o e-mail de forma assíncrona
                    await client.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    // Trate erros de envio de e-mail
                    throw new InvalidOperationException($"Erro ao enviar e-mail: {ex.Message}");
                }
            }
        }
    }
}