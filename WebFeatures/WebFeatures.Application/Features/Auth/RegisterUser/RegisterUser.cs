using FluentValidation;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Auth.RegisterUser
{
    public class RegisterUser : ICommand<UserInfoDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public class Validator : AbstractValidator<RegisterUser>
        {
            public Validator()
            {
            }
        }
    }
}
