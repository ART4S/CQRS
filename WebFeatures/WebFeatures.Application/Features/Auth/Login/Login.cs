using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Auth.Login
{
    public class Login : ICommand<UserInfoDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
