using Shouldly;
using System;
using WebFeatures.Infrastructure.Security;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Unit.Security
{
    public class PasswordHasherTests
    {
        [Fact]
        public void ComputeHash_ReturnsNonEmptyHash()
        {
            // Arrange
            string password = "12345";
            PasswordHasher hasher = new PasswordHasher();

            // Act
            string hash = hasher.ComputeHash(password);

            // Assert
            hash.ShouldNotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ComputeHash_WhenInvalidPassword_Throws(string password)
        {
            // Arrange
            PasswordHasher hasher = new PasswordHasher();

            // Act
            void actual() => hasher.ComputeHash(password);

            // Assert
            Assert.Throws<ArgumentException>(actual);
        }

        [Fact]
        public void Verify_WhenValidHashAndPassword_ReturnsTrue()
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

        [Theory]
        [InlineData(null, null)]
        [InlineData(" ", "0")]
        [InlineData("0", "")]
        [InlineData("1234", "3iH0cm1W/0u/KK9xg8ntK5KmP0q/TtXhmy6eX6WRDVB/qIMwlsSzoJtWO6ShyKn/")]
        public void Verify_WhenInvalidHashOrPassword_ReturnsFalse(string hash, string password)
        {
            // Arrange
            PasswordHasher hasher = new PasswordHasher();

            // Act
            bool isVerified = hasher.Verify(hash, password);

            // Assert
            isVerified.ShouldBeFalse();
        }
    }
}
