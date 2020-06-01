using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;

namespace WebFeatures.Application.Interfaces.DataAccess.Reading
{
    public interface IReadDbContext
    {
        IProductReadRepository Products { get; }
    }
}