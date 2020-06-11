using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Domian.Entities.Permissions;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class RoleWriteRepository : WriteRepository<Role>, IRoleWriteRepository
    {
        public RoleWriteRepository(IDbConnection connection, IEntityProfile profile) : base(connection, profile)
        {
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            string sql =
                $@"SELECT * FROM {Entity.Table.NameWithSchema()} 
                WHERE {Entity.Column(x => x.Name)} = @name";

            return (await Connection.QueryAsync<Role>(sql, new { name })).SingleOrDefault();
        }
    }
}