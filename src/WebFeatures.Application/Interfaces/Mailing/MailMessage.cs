using WebFeatures.Common;

namespace WebFeatures.Application.Interfaces.Mailing
{
	public class MailMessage
	{
		public MailMessage(string to, string subject, string body)
		{
			Guard.ThrowIfNull(to, nameof(to));
			Guard.ThrowIfNull(subject, nameof(subject));
			Guard.ThrowIfNull(body, nameof(body));

			To = to;
			Subject = subject;
			Body = body;
		}

		public string To { get; }
		public string Subject { get; }
		public string Body { get; }
	}
}
