using Dapper;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Mappings.Queries;
using WebFeatures.Infrastructure.DataAccess.Mappings.QueryProviders;

namespace WebFeatures.Infrastructure.DataAccess.Repositories
{
    internal class UserRepository : Repository<User, UserQueries>, IUserRepository
    {
        public UserRepository(
            IDbConnection connection,
            IQueryProvider<User, UserQueries> queryProvider) : base(connection, queryProvider)
        {
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return Connection.QuerySingleOrDefaultAsync<User>(Queries.GetUserByEmail, new User { Email = email });
        }
    }
}
