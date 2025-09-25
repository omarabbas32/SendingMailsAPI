using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace SendingMailsAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _settings;

        public EmailService(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Wink Customer 🚨", _settings.Username));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var builder = new BodyBuilder
            {
                TextBody = body
            };
            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.Username, _settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
