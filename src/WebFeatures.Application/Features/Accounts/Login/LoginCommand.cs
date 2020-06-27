using System;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Accounts.Login
{
    /// <summary>
    /// Войти в систему
    /// </summary>
    public class LoginCommand : ICommand<Guid>
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