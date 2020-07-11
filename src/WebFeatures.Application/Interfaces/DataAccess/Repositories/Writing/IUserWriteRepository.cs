using System.Threading.Tasks;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing
{
	public interface IUserWriteRepository : IWriteRepository<User>
	{
		Task<User> GetByEmailAsync(string email);
	}
}
