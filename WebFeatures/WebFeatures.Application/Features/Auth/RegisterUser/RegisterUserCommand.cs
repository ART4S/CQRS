using FluentValidation;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;

namespace WebFeatures.Application.Features.Auth.RegisterUser
{
    public class RegisterUserCommand : ICommand<Unit>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public class Validator : AbstractValidator<RegisterUserCommand>
        {
            public Validator()
            {
            }
        }
    }
}
