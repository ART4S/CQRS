using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Accounts.Login;
using WebFeatures.Application.Tests.Common.Base;
using WebFeatures.Domian.Entities;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Accounts.Login
{
    public class LoginCommandTests : IntegrationTestBase
    {
        [Fact]
        public async Task HandleAsync_ReturnsExistingUserId()
        {
            // Arrange
            var request = new LoginCommand
            {
                Email = "admin@mail.com",
                Password = "12345"
            };

            Guid expectedId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

            // Act
            Guid actualId = await Mediator.SendAsync(request);

            User user = await DbContext.Users.GetAsync(expectedId);

            // Assert
            actualId.Should().Be(expectedId);
            actualId.Should().Be(user.Id);

            user.Email.Should().Be(request.Email);
        }

        [Theory]
        [InlineData("user@mail.com", "1234")]
        [InlineData("user_1@mail.com", "12345")]
        public async Task HandleAsync_WhenInvalidCredentials_Throws(string email, string password)
        {
            // Arrange
            var request = new LoginCommand()
            {
                Email = email,
                Password = password
            };

            // Act
            Func<Task<Guid>> actual = () => Mediator.SendAsync(request);

            // Assert
            await actual.Should().ThrowAsync<ValidationException>();
        }
    }
}