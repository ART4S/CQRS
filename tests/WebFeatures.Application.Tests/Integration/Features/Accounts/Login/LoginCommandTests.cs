using System;
using System.Threading.Tasks;
using FluentAssertions;
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
		public async Task ShouldReturnUserId()
		{
			// Arrange
			var request = new LoginCommand
			{
				Email = "admin@mail.com",
				Password = "12345"
			};

			// Act
			Guid userId = await Mediator.SendAsync(request);

			User user = await DbContext.Users.GetByEmailAsync(request.Email);

			// Assert
			user.Should().NotBeNull();
			user.Id.Should().Be(userId);
			user.Email.Should().Be(request.Email);
		}
		
		[Theory]
		[InlineData("user@mail.com", "1234")]
		[InlineData("user_1@mail.com", "12345")]
		public void ShouldThrow_WhenInvalidCredentials(string email, string password)
		{
			// Arrange
			var request = new LoginCommand
			{
				Email = email,
				Password = password
			};

			// Act
			Func<Task<Guid>> act = () => Mediator.SendAsync(request);

			// Assert
			act.Should().Throw<ValidationException>();
		}
	}
}
