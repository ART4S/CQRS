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
        private readonly IMongoDatabase _db;

        public MongoDbReadContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _db = client.GetDatabase(settings.DatabaseName);
        }

        public async Task<IList<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : BaseEntity
        {
            string tableName = TableNamesProvider.GetTableNameForType(typeof(TEntity));

            return await (await _db.GetCollection<TEntity>(tableName)
               .FindAsync(filter ?? (x => true)))
               .ToListAsync();
        }
    }
}
