using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Domian.Entities.Permissions;

namespace WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing
{
    public interface IRoleWriteRepository : IWriteRepository<Role>
    {
        Task<Role> GetByNameAsync(string name);
    }
}