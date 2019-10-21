using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using WebFeatures.Application.Infrastructure.Exceptions;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Authentication.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, Claim[]>
    {
        private readonly IAppContext _context;
        private readonly IDataProtector _protector;

        public LoginCommandHandler(IDataProtectionProvider protectionProvider, IAppContext context)
        {
            _context = context;
            _protector = protectionProvider.CreateProtector("UserPassword");
        }

        public Claim[] Handle(LoginCommand input)
        {
            var user = _context.Set<User>()
                .AsNoTracking()
                .FirstOrDefault(x => x.Name == input.Name);

            if (user == null)
            {
                throw new ValidationException(ValidationErrorMessages.InvalidLoginOrPassword);
            }

            var password = _protector.Unprotect(user.PasswordHash);
            if (password != input.Password)
            {
                throw new ValidationException(ValidationErrorMessages.InvalidLoginOrPassword);
            }

            return new[] { new Claim(ClaimTypes.Name, user.Name) };
        }
    }
}
