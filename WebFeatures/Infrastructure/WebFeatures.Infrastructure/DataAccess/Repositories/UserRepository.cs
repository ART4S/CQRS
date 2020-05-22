using Dapper;
using System.Collections.Generic;
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

            IEnumerable<User> users = (await Connection.QueryAsync<User, UserRole, Role, User>(
                sql: sql.Query,
                map: (user, userrole, role) =>
                {
                    user.UserRoles.Add(userrole);
                    return user;
                },
                param: sql.Param,
                splitOn: sql.SplitOn));

            return users.FirstOrDefault();
        }
    }
}