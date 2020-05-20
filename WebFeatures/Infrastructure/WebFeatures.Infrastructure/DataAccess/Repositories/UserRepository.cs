using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Infrastructure.DataAccess.Repositories
{
    internal class UserRepository : BaseRepository<User>
    {
        public UserRepository(IDbConnection connection) : base(connection)
        {
        }

        public override Task<IEnumerable<User>> GetAllAsync()
        {
            string sql = "SELECT * FROM Users";

            return Connection.QueryAsync<User>(sql);
        }

        public override async Task<User> GetAsync(Guid id)
        {
            string sql = "SELECT * FROM Users WHERE Id = @id";

            User user = await Connection.QuerySingleOrDefaultAsync<User>(sql, new { id });

            return user;
        }

        public override Task CreateAsync(User entity)
        {
            if (entity.Id == default)
            {
                entity.Id = Guid.NewGuid();
            }

            string sql = "INSERT INTO Users VALUES (@Id, @Name, @Email, @PasswordHash, @PictureId)";

            return Connection.ExecuteAsync(sql, entity);
        }

        public override Task UpdateAsync(User entity)
        {
            string sql =
                "UPDATE Users\n" +
                "SET Id = @Id, Name = @Name, Email = @Email, PasswordHash = @PasswordHash, PictureId = @PictureId\n" +
                "WHERE Id = @Id";

            return Connection.ExecuteAsync(sql, entity);
        }

        public override Task DeleteAsync(User entity)
        {
            string sql = "DELETE FROM Users WHERE Id = @Id";

            return Connection.ExecuteAsync(sql, entity);
        }

        public override async Task<bool> ExistsAsync(Guid id)
        {
            string sql = "SELECT 1 FROM Users WHERE Id = @id";

            int result = await Connection.ExecuteScalarAsync<int>(sql, new { id });

            return result == 1;
        }
    }
}
