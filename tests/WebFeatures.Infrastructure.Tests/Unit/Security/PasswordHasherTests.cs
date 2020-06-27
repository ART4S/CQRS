using Bogus;
using FluentAssertions;
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
            string password = new Faker().Internet.Password();

            var sut = new PasswordHasher();

            // Act
            string hash = sut.ComputeHash(password);

            // Assert
            hash.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ComputeHash_WhenInvalidPassword_Throws(string password)
        {
            // Arrange
            var sut = new PasswordHasher();

            // Act
            Action actual = () => sut.ComputeHash(password);

            // Assert
            actual.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Verify_WhenValidHashAndPassword_ReturnsTrue()
        {
            // Arrange
            string hash = "3iH0cm1W/0u/KK9xg8ntK5KmP0q/TtXhmy6eX6WRDVB/qIMwlsSzoJtWO6ShyKn/";
            string password = "12345";

            var sut = new PasswordHasher();

            // Act
            bool result = sut.Verify(hash, password);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(" ", "0")]
        [InlineData("0", "")]
        [InlineData("1234", "3iH0cm1W/0u/KK9xg8ntK5KmP0q/TtXhmy6eX6WRDVB/qIMwlsSzoJtWO6ShyKn/")]
        public void Verify_WhenInvalidHashOrPassword_ReturnsFalse(string hash, string password)
        {
            // Arrange
            var sut = new PasswordHasher();

            // Act
            bool result = sut.Verify(hash, password);

            // Assert
            result.Should().BeFalse();
        }
    }
}