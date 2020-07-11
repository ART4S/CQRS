using System.Data.Common;

namespace WebFeatures.Application.Interfaces.DataAccess.Contexts
{
	public interface IDbContext
	{
		DbConnection Connection { get; }
	}
}
