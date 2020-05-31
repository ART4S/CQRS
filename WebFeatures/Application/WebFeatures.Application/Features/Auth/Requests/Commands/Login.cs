using WebFeatures.Application.Features.Auth.Dto;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Auth.Requests.Commands
{
    public class Login : ICommand<UserInfoDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
