using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Repositories
{
    internal class UserRepository : Repository<User, UserQueryBuilder>, IUserRepository
    {
        public UserRepository(
            IDbConnection connection,
            UserQueryBuilder queryBuilder) : base(connection, queryBuilder)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            SqlQuery sql = QueryBuilder.BuildGetUserByEmail(email);

            User user = (await Connection.QueryAsync<User, UserRole, User>(
                sql.Query,
                (user, userrole) =>
                {
                    user.UserRoles.Add(userrole);
                    return user;
                },
                sql.Param))
                .FirstOrDefault();

            return user;
        }
    }
}