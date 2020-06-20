using FluentValidation;
using System;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Accounts.Requests.Commands
{
    /// <summary>
    /// Зарегистрировать новго пользователя
    /// </summary>
    public class Register : ICommand<Guid>
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        public class Validator : AbstractValidator<Register>
        {
            public Validator()
            {
                // TODO
            }
        }
    }
}
