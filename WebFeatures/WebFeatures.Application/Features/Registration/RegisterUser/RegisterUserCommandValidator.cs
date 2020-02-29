using FluentValidation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.Security;

namespace WebFeatures.Application.Features.Registration.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(IAuthService authService)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MinimumLength(3).WithMessage("Имя должно состоять минимум из 3-х символов")
                .MaximumLength(15).WithMessage("Имя должно состоять максимум из 15-и символов")
                .Matches(@"^[A-Za-z0-9#?!@$%^&*-]+$").WithMessage("Имя может содержать буквы, цифры и следующие символы:'#?!@$%^&*-'");

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("Пароль должен состоять минимум из 8 символов")
                .Matches(@"[A-Z]").WithMessage("Пароль должен содержать минимум 1 букву в верхнем регистре")
                .Matches(@"[a-z]").WithMessage("Пароль должен содержать минимум 1 букву в нижнем регистре")
                .Matches(@"\d").WithMessage("Пароль должен содержать минимум 1 цифру")
                .Matches(@"[#?!@$%^&*-]").WithMessage("Пароль должен содержать минимум 1 из специальных '#?!@$%^&*-'");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Подтверждение пароля не совпадает");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .EmailAddress().WithMessage("Некорректный e-mail")
                .Must(authService.LoginExists).WithMessage("Пользователь с данным e-mail уже зарегистрирован");
        }
    }
}
