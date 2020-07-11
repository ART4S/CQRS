using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using WebFeatures.Application.Interfaces.Mailing;

namespace WebFeatures.Infrastructure.Mailing
{
	internal class SmtpEmailSender : IEmailSender
	{
		private readonly SmtpClientSettings _options;

		public SmtpEmailSender(IOptions<SmtpClientSettings> options)
		{
			_options = options.Value;
		}

		public async Task SendEmailAsync(MailMessage message, CancellationToken cancellationToken)
		{
			var msg = new MimeMessage();

			msg.From.Add(new MailboxAddress("", _options.Address));
			msg.To.Add(new MailboxAddress("", message.To));
			msg.Subject = message.Subject;
			msg.Body = new TextPart(TextFormat.Html) {Text = message.Body};

			using SmtpClient client = new SmtpClient();

			await client.ConnectAsync(_options.Host, _options.Port, _options.UseSsl, cancellationToken);
			await client.AuthenticateAsync(_options.Address, _options.Password, cancellationToken);
			await client.SendAsync(msg, cancellationToken);
			await client.DisconnectAsync(true, cancellationToken);
		}
	}
}
