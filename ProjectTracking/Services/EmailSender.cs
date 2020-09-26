using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ProjectTracking.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailSender : IEmailSender
    {

        private readonly EmailSettings _emailSettings;

        public EmailSender(
            IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = _emailSettings.Port;
                client.Host = _emailSettings.Host;
                client.EnableSsl = true;
                //client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);

           //     ServicePointManager.ServerCertificateValidationCallback =
           // delegate (object sender, X509Certificate certificate, X509Chain chain,
           //SslPolicyErrors sslPolicyErrors)
           // { return true; };

                var mailMessage = new MailMessage(_emailSettings.SenderEmail, email)
                {
                    Body = message,
                    Subject = subject,
                    IsBodyHtml = true,
                };

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }
    }

    public class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
    }

}
