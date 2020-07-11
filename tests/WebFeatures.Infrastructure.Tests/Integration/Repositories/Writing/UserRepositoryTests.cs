using Bogus;
using Dapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Executors;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Common.Base;
using WebFeatures.Infrastructure.Tests.Common.Stubs.Entities;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Writing
{
    public class UserRepositoryTests : IntegrationTestBase
    {
        private UserWriteRepository CreateDefaultRepository()
        {
            return new UserWriteRepository(Database.Connection, new DapperDbExecutor(), new EntityProfile());
        }

        [Fact]
        public async Task GetAllAsync_ReturnsNonEmptyCollection()
        {
            // Arrange
            UserWriteRepository sut = CreateDefaultRepository();

            // Act
            IEnumerable<User> users = await sut.GetAllAsync();

            // Assert
            users.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAsync_WhenUserExists_ReturnsUser()
        {
            // Arrange
            UserWriteRepository sut = CreateDefaultRepository();

            Guid userId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

            // Act
            User user = await sut.GetAsync(userId);

            // Assert
            user.Should().NotBeNull();
            user.Id.Should().Be(userId);
        }

        [Fact]
        public async Task GetAsync_WhenUserDoesntExists_ReturnsNull()
        {
            // Arrange
            UserWriteRepository sut = CreateDefaultRepository();

            Guid userId = new Guid("b3184c02-8126-4d3b-b039-5a957163a721");

            // Act
            User user = await sut.GetAsync(userId);

            // Assert
            user.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_CreatesOneUser()
        {
            // Arrange
            UserWriteRepository sut = CreateDefaultRepository();

            User user = new UserStub();

            Task<int> GetUsersCount() => Database.Connection.ExecuteScalarAsync<int>(
                sql: "SELECT Count(*) FROM public.users WHERE id = @Id",
                param: new { user.Id });

            // Act
            int usersCountBefore = await GetUsersCount();

            await sut.CreateAsync(user);

            int usersCountAfter = await GetUsersCount();

            // Assert
            usersCountBefore.Should().Be(0);
            usersCountAfter.Should().Be(1);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingUser()
        {
            // Arrange
            UserWriteRepository sut = CreateDefaultRepository();

            User user = new UserStub();

            user.Id = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

            Task<User> GetUser() => Database.Connection.QuerySingleAsync<User>(
                sql: $"SELECT * FROM public.users WHERE id = @Id",
                param: user);

            // Act
            User beforeUpdateUser = await GetUser();

            await sut.UpdateAsync(user);

            User afterUpdateUser = await GetUser();

            // Assert
            user.Id.Should().Be(beforeUpdateUser.Id);
            user.Id.Should().Be(afterUpdateUser.Id);
            beforeUpdateUser.Name.Should().NotBe(afterUpdateUser.Name);
        }

        [Fact]
        public async Task DeleteAsync_RemovesExistingUser()
        {
            // Arrange
            UserWriteRepository sut = CreateDefaultRepository();

            var user = new User() { Id = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09") };

            Task<int> GetUsersCount() => Database.Connection.ExecuteScalarAsync<int>(
                sql: "SELECT Count(*) FROM public.users WHERE id = @Id",
                param: new { user.Id });

            // Act

            int usersCountBefore = await GetUsersCount();

            await sut.DeleteAsync(user);

            int usersCountAfter = await GetUsersCount();

            // Assert
            usersCountBefore.Should().Be(1);
            usersCountAfter.Should().Be(0);
        }

        [Fact]
        public async Task ExistsAsync_WhenUserExists_ReturnsTrue()
        {
            // Arrange
            UserWriteRepository sut = CreateDefaultRepository();

            Guid userId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

            // Act
            bool result = await sut.ExistsAsync(userId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ExistsAsync_WhenUserDoesntExist_ReturnsFalse()
        {
            // Arrange
            UserWriteRepository sut = CreateDefaultRepository();

            Guid userId = new Guid("b3184c02-8126-4d3b-b039-5a957163a721");

            // Act
            bool result = await sut.ExistsAsync(userId);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetUserByEmailAsync_WhenUserWithEmailExists_ReturnsUser()
        {
            // Arrange
            UserWriteRepository sut = CreateDefaultRepository();

            string email = "admin@mail.com";

            // Act
            User user = await sut.GetByEmailAsync(email);

            // Assert
            user.Should().NotBeNull();
            user.Email.Should().Be(email);
        }

        [Fact]
        public async Task GetUserByEmailAsync_WhenPassedNonexistentEmail_ReturnsNull()
        {
            // Arrange
            UserWriteRepository sut = CreateDefaultRepository();

            string email = new Faker().Internet.Email();

            // Act
            User user = await sut.GetByEmailAsync(email);

            // Assert
            user.Should().BeNull();
        }
    }
}