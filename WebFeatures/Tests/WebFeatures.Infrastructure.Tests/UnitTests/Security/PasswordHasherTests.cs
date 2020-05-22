using Shouldly;
using WebFeatures.Infrastructure.Security;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.UnitTests.Security
{
    public class PasswordHasherTests
    {
        [Fact]
        public void ComputeHash_ShouldNotReturnNullOrEmptyHash()
        {
            // Arrange
            string password = "12345";
            PasswordHasher hasher = new PasswordHasher();

            // Act
            string hash = hasher.ComputeHash(password);

            // Assert
            hash.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public void Verify_ShouldVerifyComputedHash()
        {
            // Arrange
            string password = "12345";
            PasswordHasher hasher = new PasswordHasher();

            // Act
            string hash = hasher.ComputeHash(password);
            bool isVerified = hasher.Verify(hash, password);

            // Assert
            isVerified.ShouldBeTrue();
        }
    }
}
