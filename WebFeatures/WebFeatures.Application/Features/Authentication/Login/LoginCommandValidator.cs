using FluentValidation;
using WebFeatures.Application.Infrastructure.Validation;

namespace WebFeatures.Application.Features.Authentication.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationErrorMessages.NotEmpty);
        }
    }
}
