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
        public void Verify_ShouldVerifyHash()
        {
            // Arrange
            string hash = "3iH0cm1W/0u/KK9xg8ntK5KmP0q/TtXhmy6eX6WRDVB/qIMwlsSzoJtWO6ShyKn/";
            string password = "12345";
            PasswordHasher hasher = new PasswordHasher();

            // Act
            bool isVerified = hasher.Verify(hash, password);

            // Assert
            isVerified.ShouldBeTrue();
        }
    }
}
