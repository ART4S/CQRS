using Dapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Common;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Writing
{
    public class UserRepositoryTests : IntegrationTestBase
    {
        public UserRepositoryTests(DatabaseFixture database) : base(database)
        {
        }

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

            var user = new User()
            {
                Id = new Guid("a602e1c8-db6f-49b7-ae7c-beb48fe2755a"),
                Name = "",
                Email = "email",
                PasswordHash = "hash"
            };

            // Act
            await repo.CreateAsync(user);

            int usersCount = await Database.Connection.ExecuteScalarAsync<int>(
                "SELECT Count(*) FROM public.users WHERE id = @Id",
                new { user.Id });

            // Assert
            user.Id.ShouldNotBe(default);
            usersCount.ShouldBe(1);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingUser()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            var user = new User()
            {
                Id = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09"),
                Name = "",
                Email = "email1",
                PasswordHash = "hash"
            };

            string sql = $"SELECT * FROM public.users WHERE id = @Id";

            // Act
            User beforeUpdate = await Database.Connection.QuerySingleAsync<User>(sql, user);

            await repo.UpdateAsync(user);

            User afterUpdate = await Database.Connection.QuerySingleAsync<User>(sql, user);

            // Assert
            user.Id.ShouldBe(beforeUpdate.Id);
            user.Id.ShouldBe(afterUpdate.Id);
            beforeUpdate.Name.ShouldNotBe(afterUpdate.Name);
        }

        [Fact]
        public async Task DeleteAsync_RemovesExistingUser()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();

            var user = new User() { Id = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09") };

            // Act
            await repo.DeleteAsync(user);

            int usersCount = await Database.Connection.ExecuteScalarAsync<int>(
                "SELECT Count(*) FROM public.users WHERE id = @Id",
                new { user.Id });

            // Assert
            usersCount.ShouldBe(0);
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