using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace ControleContatos.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Email> _logger;

        public Email(IConfiguration configuration,
            ILogger<Email> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                string host = _configuration["SMTP:Host"];
                string Nome = _configuration["SMTP:Nome"];
                string userName = _configuration["SMTP:UserName"];
                string senha = _configuration["SMTP:Senha"];
                int porta = int.Parse(_configuration["SMTP:Porta"]);

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(userName, Nome)
                };

                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smpt = new SmtpClient(host, porta))
                {
                    smpt.Credentials = new NetworkCredential(userName, senha);
                    smpt.EnableSsl = true;

                    smpt.Send(mail);
                    _logger.LogInformation("E-mail enviado com sucesso.");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar e-mail: {ErrorMessage}", ex.Message);
                return false;
            }
        }
    }
}
