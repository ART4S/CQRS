using WebFeatures.Application.Features.Auth.Dto;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Auth.Requests.Commands
{
    /// <summary>
    /// Войти в систему
    /// </summary>
    public class Login : ICommand<UserInfoDto>
    {
        /// <summary>
        /// E-mail пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
