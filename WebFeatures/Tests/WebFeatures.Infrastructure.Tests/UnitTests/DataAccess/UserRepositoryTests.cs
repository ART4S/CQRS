using Dapper;
using Shouldly;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Repositories;
using WebFeatures.Infrastructure.Tests.Fixtures;
using Xunit;

namespace WebFeatures.Infrastructure.Tests.UnitTests.DataAccess
{
    public class UserRepositoryTests : IClassFixture<NpgsqlDatabaseFixture>
    {
        private readonly NpgsqlDatabaseFixture _db;
        private readonly UserRepository _repo;

        public UserRepositoryTests(NpgsqlDatabaseFixture db)
        {
            _db = db;
            _repo = new UserRepository(db.Connection);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateOneRecord()
        {
            var user = new User()
            {
                Name = "test",
                Email = "test@gmail.com",
                PasswordHash = "hash"
            };

            await _repo.CreateAsync(user);
            int usersCount = await _db.Connection.ExecuteScalarAsync<int>("SELECT Count(*) FROM Users");

            user.Id.ShouldNotBe(default);
            usersCount.ShouldBe(1);
        }
    }
}
