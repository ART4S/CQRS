using Shouldly;
using WebFeatures.Infrastructure.Security;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.UnitTests.Security
{
    public class PasswordHasherTests
    {
        private readonly PasswordHasher _hasher = new PasswordHasher();
        private const string _testPassword = "12345";

        [Fact]
        public void ComputeHash_ShouldNotReturnNullOrEmptyHash()
        {
            string hash = _hasher.ComputeHash(_testPassword);

            hash.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public void Verify_ShouldVerifyComputedHash()
        {
            string hash = _hasher.ComputeHash(_testPassword);

            _hasher.Verify(hash, _testPassword).ShouldBeTrue();
        }
    }
}
