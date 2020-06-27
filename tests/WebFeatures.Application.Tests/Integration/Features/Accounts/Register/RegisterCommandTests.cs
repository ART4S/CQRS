using FluentAssertions;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Accounts.Register;
using WebFeatures.Application.Tests.Common.Base;
using WebFeatures.Application.Tests.Common.Stubs.Requests.Accounts;
using WebFeatures.Domian.Entities;
using Xunit;

namespace WebFeatures.Application.Tests.Integration.Features.Accounts.Register
{
    public class RegisterCommandTests : IntegrationTestBase
    {
        [Fact]
        public async Task HandleAsync_CreatesNewUser()
        {
            // Arrange
            RegisterCommand request = new RegisterStub();

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
    }
}