using FluentValidation;
using WebFeatures.Application.Features.Auth.Dto;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Auth.Requests.Commands
{
    public class RegisterUser : ICommand<UserInfoDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class Validator : AbstractValidator<RegisterUser>
        {
            public Validator()
            {
                // TODO
            }
        }
    }
}
