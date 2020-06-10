using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;

namespace WebFeatures.Application.Interfaces.DataAccess.Contexts
{
    public interface IReadDbContext : IDbContext
    {
        IProductReadRepository Products { get; }
    }
}