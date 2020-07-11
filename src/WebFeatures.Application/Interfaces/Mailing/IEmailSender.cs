using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Application.Interfaces.Mailing
{
	public interface IEmailSender
	{
		Task SendEmailAsync(MailMessage message, CancellationToken cancellationToken);
	}
}
