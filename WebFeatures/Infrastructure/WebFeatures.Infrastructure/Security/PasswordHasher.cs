using System.Security.Cryptography;
using WebFeatures.Application.Interfaces.Security;

namespace WebFeatures.Infrastructure.Security
{
    internal class PasswordHasher : IPasswordHasher
    {
        private static readonly int IterationsCount = 1000;
        private static readonly int SaltSize = 128 / 8; // 128 bit
        private static readonly int SubKey = 256 / 8; // 256 bit

        private readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

        public string ComputeHash(string password)
        {
            throw new System.NotImplementedException();
        }

        public bool Verify(string hashedPassword, string expectedPassword)
        {
            throw new System.NotImplementedException();
        }
    }
}
