using System.Threading.Tasks;

namespace WebFeatures.Application.Interfaces.Security
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
