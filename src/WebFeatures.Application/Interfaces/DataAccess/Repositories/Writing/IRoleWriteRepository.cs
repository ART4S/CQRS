using System.Threading.Tasks;
using WebFeatures.Domian.Entities.Accounts;

namespace WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing
{
	public interface IRoleWriteRepository : IWriteRepository<Role>
	{
		Task<Role> GetByNameAsync(string name);
	}
}
