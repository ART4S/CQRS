using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Queries.Builders;
using WebFeatures.Infrastructure.DataAccess.Queries.Common;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class UserWriteRepository : WriteRepository<User, IUserQueryBuilder>, IUserWriteRepository
    {
        public UserWriteRepository(
            IDbConnection connection,
            IUserQueryBuilder queryBuilder) : base(connection, queryBuilder)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            SqlQuery sql = QueryBuilder.BuildGetUserByEmail(email);

            User user = (await Connection.QueryAsync<User>(sql.Query, sql.Param))
                .Distinct()
                .SingleOrDefault();

            return user;
        }
    }
}