using Microsoft.AspNetCore.DataProtection;
using WebFeatures.Application.Interfaces.Security;

namespace WebFeatures.Infrastructure.Security
{
    internal class PasswordEncoder : IPasswordEncoder
    {
        private readonly IDataProtector _protector;

        public PasswordEncoder(IDataProtectionProvider protectionProvider)
        {
            _protector = protectionProvider.CreateProtector(nameof(PasswordEncoder));
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
