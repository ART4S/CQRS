using System.Data;

namespace WebFeatures.Application.Interfaces.DataAccess.Contexts
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
        void SaveChanges();
    }
}
