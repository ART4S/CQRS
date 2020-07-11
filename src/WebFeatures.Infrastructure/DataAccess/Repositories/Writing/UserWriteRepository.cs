using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Domian.Entities;
using WebFeatures.Infrastructure.DataAccess.Executors;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        public UserWriteRepository(
            IDbConnection connection,
            IDbExecutor executor,
            IEntityProfile profile) : base(connection, executor, profile)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            string sql = string.Format(
                @"SELECT * FROM {0}
                WHERE {1} = @email",
                Entity.Table.NameWithSchema(),
                Entity.Column(x => x.Email));

            return (await Executor.QueryAsync<User>(Connection, sql, new { email })).SingleOrDefault();
        }
    }
}