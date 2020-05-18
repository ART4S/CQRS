using Shouldly;
using WebFeatures.Infrastructure.Security;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit
{
    public class PasswordHasherTests
    {
        private readonly PasswordHasher _hasher = new PasswordHasher();

        [Fact]
        public void ComputeHash_ShouldNotReturnNullOrEmptyHash()
        {
            string hash = _hasher.ComputeHash("12345");

            hash.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public void Verify_ShouldVerifyComputedHash()
        {
            string password = "12345";

            string hash = _hasher.ComputeHash(password);

            _hasher.Verify(hash, password).ShouldBeTrue();
        }
    }
}
