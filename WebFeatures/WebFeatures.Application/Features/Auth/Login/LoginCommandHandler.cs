using System;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces.Security;

namespace WebFeatures.Application.Features.Auth.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, UserDto>
    {
        private readonly IUserManager _userManager;

        public LoginCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public UserDto Handle(LoginCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
