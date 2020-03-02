using FluentValidation;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Validation;

namespace WebFeatures.Application.Features.Auth.Login
{
    public class LoginCommand : ICommand<UserDto>
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public class Validator : AbstractValidator<LoginCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);
            }
        }
    }
}
