using System.Threading.Tasks;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Interfaces.DataAccess.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
