using Microsoft.AspNetCore.DataProtection;
using WebFeatures.Application.Interfaces.Security;

namespace WebFeatures.Infrastructure.Security
{
    internal class PasswordHasher : IPasswordHasher
    {
        private readonly IDataProtector _protector;

        public PasswordHasher(IDataProtectionProvider protectionProvider)
        {
            _protector = protectionProvider.CreateProtector(nameof(PasswordHasher));
        }

        public string EncodePassword(string password)
        {
            return _protector.Protect(password);
        }

        public string DecodePassword(string password)
        {
            return _protector.Unprotect(password);
        }
    }
}
