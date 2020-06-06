using FluentValidation;
using WebFeatures.Application.Features.Accounts.Dto;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Accounts.Requests.Commands
{
    public class Register : ICommand<UserInfoDto>
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail пользователя
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
