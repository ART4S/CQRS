using System;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.Security;

namespace WebFeatures.Application.Features.Registration.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Unit>
    {
        private readonly IRegistrationService _registrationService;

        public RegisterUserCommandHandler(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public Unit Handle(RegisterUserCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
