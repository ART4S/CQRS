using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;

namespace WebFeatures.Application.Interfaces.DataAccess.Reading
{
    public interface IReadDbContext : IDbContext
    {
        IProductReadRepository Products { get; }
    }
}