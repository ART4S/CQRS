using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Features.Accounts.Requests.Commands;
using WebFeatures.Application.Tests.Common.Base;
using WebFeatures.Application.Tests.Common.Stubs.Requests.Accounts;
using WebFeatures.Domian.Entities;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Accounts
{
    public class AccountCommandHandlerTests : IntegrationTestBase
    {
        [Fact]
        public async Task Register_CreatesNewUser()
        {
            // Arrange
            Register request = new RegisterStub();

            // Act
            Guid userId = await Mediator.SendAsync(request);
            User user = await DbContext.Users.GetAsync(userId);

            // Assert
            user.Id.Should().Be(userId);
            user.Name.Should().Be(request.Name);
            user.Email.Should().Be(request.Email);
            user.PasswordHash.Should().NotBeNullOrWhiteSpace();
            user.PictureId.Should().BeNull();
        }

        [Fact]
        public async Task Login_ReturnsExistingUserId()
        {
            // Arrange
            var request = new Login
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
        public async Task Login_WhenInvalidCredentials_Throws(string email, string password)
        {
            // Arrange
            var request = new Login()
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