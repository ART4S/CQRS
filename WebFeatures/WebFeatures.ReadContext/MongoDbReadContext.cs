using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Domian.Common;

namespace WebFeatures.ReadContext
{
    public class MongoDbReadContext : IReadContext
    {
        public IMongoDatabase Database { get; }

        public MongoDbReadContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            Database = client.GetDatabase(settings.DatabaseName);
        }

        public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : BaseEntity
        {
            string tableName = TableNames.Get(typeof(TEntity));

            return await (await Database.GetCollection<TEntity>(tableName)
               .FindAsync(filter ?? (x => true)))
               .ToListAsync();
        }

        public Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
