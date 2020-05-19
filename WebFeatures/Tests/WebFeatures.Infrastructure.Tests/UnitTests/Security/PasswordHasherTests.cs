using Shouldly;
using WebFeatures.Infrastructure.Security;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.UnitTests.Security
{
    public class PasswordHasherTests
    {
        private readonly PasswordHasher _hasher = new PasswordHasher();

        [Fact]
        public void ComputeHash_ShouldNotReturnNullOrEmptyHash()
        {
            string password = "12345";

            string hash = _hasher.ComputeHash(password);

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
