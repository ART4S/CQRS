using Dapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Repositories;
using WebFeatures.Infrastructure.Tests.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.UnitTests.DataAccess.Repositories
{
    [Collection("NpgsqlDatabase")]
    public class UserRepositoryTests
    {
        private readonly NpgsqlDatabaseFixture _db;
        private readonly UserRepository _repo;

        public UserRepositoryTests(NpgsqlDatabaseFixture db)
        {
            _db = db;
            _repo = new UserRepository(db.Connection, new UserQueryBuilder(new EntityProfile()));
        }

        [Fact]
        public async Task GetAllAsync_ShouldNotReturnEmptyCollection()
        {
            // Act
            IEnumerable<User> users = await _repo.GetAllAsync();

            // Assert
            users.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnExistingUser()
        {
            // Arrange
            Guid existingUserId = new Guid("067d520f-fe3c-493c-a4c9-0bce1cf57212");

            // Act
            User user = await _repo.GetAsync(existingUserId);

            // Assert
            user.ShouldNotBeNull();
            user.Id.ShouldBe(existingUserId);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNullWhenUserDoesntExists()
        {
            // Arrange
            Guid nonExistingUserId = Guid.NewGuid();

            // Act
            User user = await _repo.GetAsync(nonExistingUserId);

            // Assert
            user.ShouldBeNull();
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateOneUser()
        {
            // Arrange
            var user = new User()
            {
                Id = new Guid("7840a202-0ba5-40ff-a803-20f227354693"),
                Name = "",
                Email = "",
                PasswordHash = ""
            };

            // Act
            await _repo.CreateAsync(user);

            int usersCount = await _db.Connection.ExecuteScalarAsync<int>(
                "SELECT Count(*) FROM Users WHERE Id = @Id",
                new { user.Id });

            // Assert
            user.Id.ShouldNotBe(default);
            usersCount.ShouldBe(1);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser()
        {
            // Arrange
            var user = new User()
            {
                Id = new Guid("a6945683-34e1-46b2-a911-f0c437422b53"),
                Name = "test",
                Email = "test",
                PasswordHash = "test"
            };

            string sql = $"SELECT * FROM Users WHERE Id = @Id";

            // Act
            User beforeUpdate = await _db.Connection.QuerySingleAsync<User>(sql, user);

            await _repo.UpdateAsync(user);

            User afterUpdate = await _db.Connection.QuerySingleAsync<User>(sql, user);

            // Assert
            user.Id.ShouldBe(beforeUpdate.Id);
            user.Id.ShouldBe(afterUpdate.Id);
            beforeUpdate.Name.ShouldNotBe(afterUpdate.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteExistingUser()
        {
            // Arrange
            var existingUser = new User() { Id = new Guid("0de81728-e359-4925-b94b-acd539e7ad3c") };

            // Act
            await _repo.DeleteAsync(existingUser);

            int usersCount = await _db.Connection.ExecuteScalarAsync<int>(
                "SELECT Count(*) FROM Users WHERE Id = @Id",
                new { existingUser.Id });

            // Assert
            usersCount.ShouldBe(0);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrueWhenRecordExists()
        {
            // Arrange
            Guid existingUserId = new Guid("1dfae12a-c1a6-47aa-b73f-e44e339d16f1");

            // Act
            bool recordExists = await _repo.ExistsAsync(existingUserId);

            // Assert
            recordExists.ShouldBeTrue();
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnFalseWhenUserDoesntExist()
        {
            // Arrange
            Guid nonExistingUserId = Guid.NewGuid();

            // Act
            bool recordExists = await _repo.ExistsAsync(nonExistingUserId);

            // Assert
            recordExists.ShouldBeFalse();
        }

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnUserWithEmail()
        {
            // Arrange
            string existingEmail = "test@mail.com";

            // Act
            User user = await _repo.GetByEmailAsync(existingEmail);

            // Assert
            user.ShouldNotBeNull();
            user.Email.ShouldBe(existingEmail);

            user.UserRoles.Count.ShouldBe(2);
            user.UserRoles.ShouldAllBe(x => x.User == user);
            user.UserRoles.ShouldAllBe(x => x.RoleId == x.Role.Id);
        }
    }
}
