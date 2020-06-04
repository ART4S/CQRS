using System.Data;

namespace WebFeatures.Application.Interfaces.DataAccess
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
        IDbTransaction BeginTransaction();
    }
}
