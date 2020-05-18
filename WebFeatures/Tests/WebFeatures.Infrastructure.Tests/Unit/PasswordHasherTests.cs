using Shouldly;
using WebFeatures.Infrastructure.Security;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit
{
    public class PasswordHasherTests
    {
        private readonly PasswordHasher _hasher = new PasswordHasher();
        private const string _password = "password";

        [Fact]
        public void ComputeHash_ShouldNotReturnNullOrEmptyHash()
        {
            string hash = _hasher.ComputeHash(_password);

            hash.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public void Verify_ShouldVerifyComputedHash()
        {
            string hash = _hasher.ComputeHash(_password);

            _hasher.Verify(hash, _password).ShouldBeTrue();
        }
    }
}
