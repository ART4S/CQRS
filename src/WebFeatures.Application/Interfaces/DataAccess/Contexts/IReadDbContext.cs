using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Reading;

namespace WebFeatures.Application.Interfaces.DataAccess.Contexts
{
    public interface IReadDbContext : IDbContext
    {
        IProductReadRepository Products { get; }
        IFileReadRepository Files { get; }
    }
}