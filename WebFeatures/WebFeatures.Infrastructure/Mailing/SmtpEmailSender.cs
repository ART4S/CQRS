using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Infrastructure.Mailing
{
    internal class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpClientOptions _options;

        public SmtpEmailSender(IOptions<SmtpClientOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var msg = new MimeMessage();

            msg.From.Add(new MailboxAddress(_options.Address));
            msg.To.Add(new MailboxAddress(to));
            msg.Subject = subject;
            msg.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var client = new SmtpClient();

            await client.ConnectAsync(_options.Host, _options.Port, _options.UseSsl);
            await client.AuthenticateAsync(_options.Address, _options.Password);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
        }
    }
}
