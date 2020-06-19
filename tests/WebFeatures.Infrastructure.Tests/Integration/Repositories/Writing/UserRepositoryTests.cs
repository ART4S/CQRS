using Bogus;
using Dapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Common.Base;
using WebFeatures.Infrastructure.Tests.Common.Factories;
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
            UserWriteRepository repo = CreateDefaultRepository();

            // Act
            IEnumerable<User> users = await repo.GetAllAsync();

            // Assert
            users.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetAsync_WhenUserExists_ReturnsUser()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            Guid userId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

            // Act
            User user = await repo.GetAsync(userId);

            // Assert
            user.ShouldNotBeNull();
            user.Id.ShouldBe(userId);
        }

        [Fact]
        public async Task GetAsync_WhenUserDoesntExists_ReturnsNull()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            Guid userId = new Guid("b3184c02-8126-4d3b-b039-5a957163a721");

            // Act
            User user = await repo.GetAsync(userId);

            // Assert
            user.ShouldBeNull();
        }

        [Fact]
        public async Task CreateAsync_CreatesOneUser()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            User user = UserFactory.Get();

            string usersCountSql = "SELECT Count(*) FROM public.users WHERE id = @Id";

            // Act
            int usersCountBefore = await Database.Connection.ExecuteScalarAsync<int>(usersCountSql, new { user.Id });

            await repo.CreateAsync(user);

            int usersCountAfter = await Database.Connection.ExecuteScalarAsync<int>(usersCountSql, new { user.Id });

            // Assert
            usersCountBefore.ShouldBe(0);
            usersCountAfter.ShouldBe(1);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingUser()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            User user = UserFactory.Get();

            user.Id = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

            string selectUserSql = $"SELECT * FROM public.users WHERE id = @Id";

            // Act
            User beforeUpdateUser = await Database.Connection.QuerySingleAsync<User>(selectUserSql, user);

            await repo.UpdateAsync(user);

            User afterUpdateUser = await Database.Connection.QuerySingleAsync<User>(selectUserSql, user);

            // Assert
            user.Id.ShouldBe(beforeUpdateUser.Id);
            user.Id.ShouldBe(afterUpdateUser.Id);
            beforeUpdateUser.Name.ShouldNotBe(afterUpdateUser.Name);
        }

        [Fact]
        public async Task DeleteAsync_RemovesExistingUser()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            var user = new User() { Id = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09") };

            string usersCountSql = "SELECT Count(*) FROM public.users WHERE id = @Id";

            // Act

            int usersCountBefore = await Database.Connection.ExecuteScalarAsync<int>(usersCountSql, new { user.Id });

            await repo.DeleteAsync(user);

            int usersCountAfter = await Database.Connection.ExecuteScalarAsync<int>(usersCountSql, new { user.Id });

            // Assert
            usersCountBefore.ShouldBe(1);
            usersCountAfter.ShouldBe(0);
        }

        [Fact]
        public async Task ExistsAsync_WhenUserExists_ReturnsTrue()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            Guid userId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

            // Act
            bool result = await repo.ExistsAsync(userId);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task ExistsAsync_WhenUserDoesntExist_ReturnsFalse()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            Guid userId = new Guid("b3184c02-8126-4d3b-b039-5a957163a721");

            // Act
            bool result = await repo.ExistsAsync(userId);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public async Task GetUserByEmailAsync_WhenUserWithEmailExists_ReturnsUser()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            string email = "admin@mail.com";

            // Act
            User user = await repo.GetByEmailAsync(email);

            // Assert
            user.ShouldNotBeNull();
            user.Email.ShouldBe(email);
        }

        [Fact]
        public async Task GetUserByEmailAsync_WhenPassedInvalidEmail_ReturnsNull()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            string email = "invalid@mail.com";

            // Act
            User user = await repo.GetByEmailAsync(email);

            // Assert
            user.ShouldBeNull();
        }
    }
}