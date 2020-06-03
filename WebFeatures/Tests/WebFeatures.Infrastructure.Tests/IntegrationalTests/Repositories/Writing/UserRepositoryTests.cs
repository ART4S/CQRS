using Dapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Repositories.Writing;
using WebFeatures.Infrastructure.Tests.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.IntegrationalTests.Repositories.Writing
{
    [Collection("PostgreSqlDatabase")]
    public class UserRepositoryTests
    {
        private readonly PostgreSqlDatabaseFixture _db;
        private readonly UserWriteRepository _repo;

        public UserRepositoryTests(PostgreSqlDatabaseFixture db)
        {
            _db = db;

            var profile = new EntityProfile();

            profile.RegisterMappingsFromAssembly(typeof(UserMap).Assembly);

            _repo = new UserWriteRepository(db.Connection, new UserQueryBuilder(profile));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnNonEmptyCollection()
        {
            // Act
            IEnumerable<User> users = await _repo.GetAllAsync();

            // Assert
            users.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnUser_IfUserExists()
        {
            // Arrange
            Guid userId = new Guid("067d520f-fe3c-493c-a4c9-0bce1cf57212");

            // Act
            User user = await _repo.GetAsync(userId);

            // Assert
            user.ShouldNotBeNull();
            user.Id.ShouldBe(userId);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_IfUserDoesntExists()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            // Act
            User user = await _repo.GetAsync(userId);

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
        public async Task UpdateAsync_ShouldUpdateExistingUser()
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
            var user = new User() { Id = new Guid("0de81728-e359-4925-b94b-acd539e7ad3c") };

            // Act
            await _repo.DeleteAsync(user);

            int usersCount = await _db.Connection.ExecuteScalarAsync<int>(
                "SELECT Count(*) FROM Users WHERE Id = @Id",
                new { user.Id });

            // Assert
            usersCount.ShouldBe(0);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrue_IfUserExists()
        {
            // Arrange
            Guid userId = new Guid("1dfae12a-c1a6-47aa-b73f-e44e339d16f1");

            // Act
            bool recordExists = await _repo.ExistsAsync(userId);

            // Assert
            recordExists.ShouldBeTrue();
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnFalse_IfUserDoesntExist()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            // Act
            bool recordExists = await _repo.ExistsAsync(userId);

            // Assert
            recordExists.ShouldBeFalse();
        }

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnUser_IfUserWithEmailExists()
        {
            // Arrange
            string email = "test@mail.com";

            // Act
            User user = await _repo.GetByEmailAsync(email);

            // Assert
            user.ShouldNotBeNull();
            user.Email.ShouldBe(email);
        }
    }
}