using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Application.Interfaces.DataAccess.Contexts
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
