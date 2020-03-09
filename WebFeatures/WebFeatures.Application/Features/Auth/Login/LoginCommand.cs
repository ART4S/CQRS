using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Application.Features.Auth.Login
{
    public class LoginCommand : ICommand<UserInfoDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class Validator : AbstractValidator<LoginCommand>
        {
            public Validator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
