using Dapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Common;
using WebFeatures.Infrastructure.Tests.Common.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.Integration.Repositories.Writing
{
    public class UserRepositoryTests : IntegrationTestBase
    {
        private readonly IDbConnection _connection;

        public UserRepositoryTests(DatabaseFixture db) : base(db)
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
            Guid userId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

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
                Id = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09"),
                Name = "",
                Email = "",
                PasswordHash = ""
            };

            string sql = $"SELECT * FROM public.users WHERE id = @Id";

            // Act
            User notUpdatedUser = await _connection.QuerySingleAsync<User>(sql, user);

            await repo.UpdateAsync(user);

            User updatedUser = await _connection.QuerySingleAsync<User>(sql, user);

            // Assert
            user.Id.ShouldBe(notUpdatedUser.Id);
            user.Id.ShouldBe(updatedUser.Id);
            notUpdatedUser.Name.ShouldNotBe(updatedUser.Name);
        }

        [Fact]
        public async Task DeleteAsync_DeletesExistingUser()
        {
            // Arrange
            UserWriteRepository repo = CreateDefaultRepository();
            var user = new User() { Id = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09") };

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
            Guid userId = new Guid("a91e29b7-813b-47a3-93f0-8ad34d4c8a09");

            // Act
            bool result = await repo.ExistsAsync(userId);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task ExistsAsync_ReturnsFalse_IfUserDoesntExist()
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
        public async Task GetUserByEmailAsync_ReturnsUser_IfUserWithPassedEmailExists()
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
    }
}