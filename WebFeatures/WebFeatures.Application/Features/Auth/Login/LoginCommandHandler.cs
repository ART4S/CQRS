using System;
using System.Security.Claims;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.Security;

namespace WebFeatures.Application.Features.Auth.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, Claim[]>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Claim[] Handle(LoginCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
