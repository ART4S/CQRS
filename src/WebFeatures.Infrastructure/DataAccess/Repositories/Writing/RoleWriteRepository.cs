using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Domian.Entities.Accounts;
using WebFeatures.Infrastructure.DataAccess.Executors;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class RoleWriteRepository : WriteRepository<Role>, IRoleWriteRepository
    {
        public RoleWriteRepository(
            IDbConnection connection,
            IDbExecutor executor,
            IEntityProfile profile) : base(connection, executor, profile)
        {
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            string sql =
                $@"SELECT * FROM {Entity.Table.NameWithSchema()} 
                WHERE {Entity.Column(x => x.Name)} = @name";

            return (await Executor.QueryAsync<Role>(Connection, sql, new { name })).SingleOrDefault();
        }
    }
}