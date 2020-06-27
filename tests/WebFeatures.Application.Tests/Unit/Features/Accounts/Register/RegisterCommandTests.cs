using Bogus;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Accounts.Register;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Tests.Common.Stubs.Entities;
using WebFeatures.Application.Tests.Common.Stubs.Requests.Accounts;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Permissions;
using Xunit;

namespace WebFeatures.Application.Tests.Unit.Features.Accounts.Register
{
    public class RegisterCommandTests
    {
        private readonly Mock<IPasswordHasher> _hasher;
        private readonly Mock<IWriteDbContext> _context;
        private readonly Mock<ILogger<RegisterCommand>> _logger;

        public RegisterCommandTests()
        {
            _hasher = new Mock<IPasswordHasher>();
            _context = new Mock<IWriteDbContext>();
            _logger = new Mock<ILogger<RegisterCommand>>();
        }

        [Fact]
        public async Task HandleAsync_CreatesNewUser()
        {
            // Arrange
            RegisterCommand request = new RegisterStub();

            string hash = new Randomizer().Utf16String();

            _hasher.Setup(x => x.ComputeHash(request.Password)).Returns(hash);

            var userRepo = new Mock<IUserWriteRepository>();

            Guid expectedUserId = new Faker().Random.Guid();

            userRepo.Setup(x => x.CreateAsync(
                    It.Is<User>(x => x.Email == request.Email && x.Name == request.Name && x.PasswordHash == hash)))
                .Callback<User>(x => x.Id = expectedUserId);

            _context.Setup(x => x.Users).Returns(userRepo.Object);

            var roleRepo = new Mock<IRoleWriteRepository>();

            roleRepo.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(new RoleStub());

            _context.Setup(x => x.Roles).Returns(roleRepo.Object);

            _context.Setup(x => x.UserRoles).Returns(Mock.Of<IWriteRepository<UserRole>>());

            var sut = new RegisterCommandHandler(_context.Object, _hasher.Object, _logger.Object);

            // Act
            Guid actualUserId = await sut.HandleAsync(request, new CancellationToken());

            // Assert
            actualUserId.Should().Be(expectedUserId);
        }

        [Fact]
        public async Task HandleAsync_WhenRoleForUserIsMissing_Throws()
        {
            // Arrange
            _context.Setup(x => x.Roles).Returns(Mock.Of<IRoleWriteRepository>());
            _context.Setup(x => x.Users).Returns(Mock.Of<IUserWriteRepository>());

            var sut = new RegisterCommandHandler(_context.Object, _hasher.Object, _logger.Object);

            // Act
            Func<Task> actual = () => sut.HandleAsync(new RegisterCommand(), new CancellationToken());

            // Assert
            await actual.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}