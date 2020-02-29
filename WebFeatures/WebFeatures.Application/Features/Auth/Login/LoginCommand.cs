using System.Security.Claims;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Auth.Login
{
    public class LoginCommand : ICommand<Claim[]>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
