using WebFeatures.Application.Interfaces.DataAccess.Reading;
using WebFeatures.Application.Interfaces.DataAccess.Reading.Repositories;
using WebFeatures.Infrastructure.DataAccess.Contexts;
using WebFeatures.Infrastructure.DataAccess.Repositories.Reading;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure.DataAccess
{
    internal class ReadDbContext : BaseDbContext, IReadDbContext
    {
        public IProductReadRepository Products => _products ??= new ProductReadRepository(Connection);
        private ProductReadRepository _products;

        public ReadDbContext(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}