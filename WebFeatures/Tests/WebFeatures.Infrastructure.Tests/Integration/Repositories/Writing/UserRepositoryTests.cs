using Dapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Writing
{
    [Collection("PostgreSqlDatabase")]
    public class UserRepositoryTests
    {
        private readonly IDbConnection _connection;

        public UserRepositoryTests(PostgreSqlDatabaseFixture db)
        {
            _connection = db.Connection;
        }

        private UserWriteRepository CreateDefaultRepository()
        {
            return new UserWriteRepository(_connection, new EntityProfile());
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
        public async Task GetAsync_ReturnsUser_IfUserExists()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();
            Guid userId = new Guid("067d520f-fe3c-493c-a4c9-0bce1cf57212");

            // Act
            User user = await repo.GetAsync(userId);

            // Assert
            user.ShouldNotBeNull();
            user.Id.ShouldBe(userId);
        }

        [Fact]
        public async Task GetAsync_ReturnsNull_IfUserDoesntExists()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();
            Guid userId = Guid.NewGuid();

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
                Id = new Guid("7840a202-0ba5-40ff-a803-20f227354693"),
                Name = "",
                Email = "",
                PasswordHash = ""
            };

            // Act
            await repo.CreateAsync(user);

            int usersCount = await _connection.ExecuteScalarAsync<int>(
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
                Id = new Guid("a6945683-34e1-46b2-a911-f0c437422b53"),
                Name = "test",
                Email = "test",
                PasswordHash = "test"
            };

            string sql = $"SELECT * FROM public.users WHERE id = @Id";

            // Act
            User beforeUpdate = await _connection.QuerySingleAsync<User>(sql, user);

            await repo.UpdateAsync(user);

            User afterUpdate = await _connection.QuerySingleAsync<User>(sql, user);

            // Assert
            user.Id.ShouldBe(beforeUpdate.Id);
            user.Id.ShouldBe(afterUpdate.Id);
            beforeUpdate.Name.ShouldNotBe(afterUpdate.Name);
        }

        [Fact]
        public async Task DeleteAsync_DeletesExistingUser()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();
            var user = new User() { Id = new Guid("0de81728-e359-4925-b94b-acd539e7ad3c") };

            // Act
            await repo.DeleteAsync(user);

            int usersCount = await _connection.ExecuteScalarAsync<int>(
                "SELECT Count(*) FROM public.users WHERE id = @Id",
                new { user.Id });

            // Assert
            usersCount.ShouldBe(0);
        }

        [Fact]
        public async Task ExistsAsync_ReturnsTrue_IfUserExists()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();
            Guid userId = new Guid("1dfae12a-c1a6-47aa-b73f-e44e339d16f1");

            // Act
            bool recordExists = await repo.ExistsAsync(userId);

            // Assert
            recordExists.ShouldBeTrue();
        }

        [Fact]
        public async Task ExistsAsync_ReturnsFalse_IfUserDoesntExist()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();
            Guid userId = Guid.NewGuid();

            // Act
            bool recordExists = await repo.ExistsAsync(userId);

            // Assert
            recordExists.ShouldBeFalse();
        }

        [Fact]
        public async Task GetUserByEmailAsync_ReturnsUser_IfUserWithPassedEmailExists()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();
            string email = "test@mail.com";

            // Act
            User user = await repo.GetByEmailAsync(email);

            // Assert
            user.ShouldNotBeNull();
            user.Email.ShouldBe(email);
        }
    }
}