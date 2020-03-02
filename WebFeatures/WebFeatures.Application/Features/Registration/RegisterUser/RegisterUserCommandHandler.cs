using System;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.Security;

namespace WebFeatures.Application.Features.Registration.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Unit>
    {
        private readonly IUserManager _userManager;

        public RegisterUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Unit Handle(RegisterUserCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
