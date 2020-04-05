using System.Threading.Tasks;

namespace WebFeatures.Application.Interfaces.Mailing
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
