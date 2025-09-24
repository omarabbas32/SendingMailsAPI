using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using NETCore.MailKit.Core;
using System.Threading.Tasks;

namespace SendingMailsAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _username = "omarabbasd89@gmail.com";      // <-- change
        private readonly string _password = "xjqw ozzw jvyo wzdd";        // <-- change

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Wink Costumer 🚨", _username));
            message.To.Add(new MailboxAddress("Wink Company", to)); // recipient name optional
            message.Subject = subject;

            var builder = new BodyBuilder
            {
                TextBody = body
            };

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_username, _password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
