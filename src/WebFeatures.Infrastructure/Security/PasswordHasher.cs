using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Common;

namespace WebFeatures.Infrastructure.Security
{
    internal class PasswordHasher : IPasswordHasher
    {
        private const int IterationsCount = 10000;
        private const int SaltSize = 128 / 8;
        private const int SubkeySize = 256 / 8;

        private readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

        public string ComputeHash(string password)
        {
            Guard.ThrowIfNullOrEmpty(password, nameof(password));

            byte[] salt = new byte[SaltSize];
            _random.GetBytes(salt);

            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, IterationsCount, SubkeySize);
            byte[] hash = new byte[SaltSize + SubkeySize];

            Array.Copy(salt, 0, hash, 0, salt.Length);
            Array.Copy(subkey, 0, hash, salt.Length, subkey.Length);

            return Convert.ToBase64String(hash);
        }

        public bool Verify(string hashedPassword, string expectedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword) || string.IsNullOrWhiteSpace(expectedPassword))
            {
                return false;
            }

            byte[] hashed = Convert.FromBase64String(hashedPassword);

            if (hashed.Length != SaltSize + SubkeySize)
            {
                return false;
            }

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashed, 0, salt, 0, salt.Length);

            byte[] subkey = new byte[SubkeySize];
            Array.Copy(hashed, salt.Length, subkey, 0, subkey.Length);

            byte[] expectedSubkey = KeyDerivation.Pbkdf2(expectedPassword, salt, KeyDerivationPrf.HMACSHA256, IterationsCount, SubkeySize);

            return ByteArraysAreEqual(subkey, expectedSubkey);
        }

        private bool ByteArraysAreEqual(byte[] first, byte[] second)
        {
            if (first.Length != second.Length)
            {
                return false;
            }

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}