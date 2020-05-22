using Dapper;
using System;
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

            var users = new Dictionary<Guid, User>();

            User user = (await Connection.QueryAsync<User, UserRole, Role, User>(
                sql: sql.Query,
                map: (user, userRole, role) =>
                {
                    if (!users.TryGetValue(user.Id, out User existingUser))
                    {
                        users.Add(user.Id, user);
                    }
                    else
                    {
                        user = existingUser;
                    }

                    if (userRole != null)
                    {
                        userRole.User = user;
                        userRole.Role = role;

                        user.UserRoles.Add(userRole);
                    }

                    return user;
                },
                param: sql.Param,
                splitOn: sql.SplitOn))
                .Distinct()
                .SingleOrDefault();

            return user;
        }
    }
}