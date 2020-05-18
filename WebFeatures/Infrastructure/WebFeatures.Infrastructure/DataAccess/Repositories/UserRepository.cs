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
        public UserRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public override Task CreateAsync(User entity)
        {
            string sql = "INSERT INTO Users VALUES (@Name, @Email, @PasswordHash, @PictureId)";

            return Connection.ExecuteAsync(sql, entity);
        }

        public override Task DeleteAsync(User entity)
        {
            string sql = "DELETE FROM Users WHERE Id == @Id";

            return Connection.ExecuteAsync(sql, entity);
        }

        public override async Task<bool> ExistsAsync(Guid id)
        {
            string sql = "SELECT 1 FROM Users WHERE Id == @Id";

            int result = await Connection.ExecuteScalarAsync<int>(sql, new { id });

            return result == 1;
        }

        public override Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
