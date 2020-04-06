using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;

namespace WebFeatures.ReadContext
{
    public class MongoReadContext : IReadContext
    {
        public IMongoDatabase Database { get; }

        public MongoReadContext(MongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            Database = client.GetDatabase(settings.DatabaseName);
        }

        public async Task<IList<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : BaseEntity
        {
            return await (await Collection<TEntity>()
                .FindAsync(filter ?? (x => true)))
                .ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : BaseEntity
        {
            TEntity entry = await (await Collection<TEntity>()
                .FindAsync(x => x.Id == id))
                .FirstOrDefaultAsync();

            return entry;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Collection<TEntity>().InsertOne(entity);
        }

        public Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return Collection<TEntity>().InsertOneAsync(entity);
        }

        public Task UpsertAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return Collection<TEntity>().ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        private IMongoCollection<TEntity> Collection<TEntity>() where TEntity : BaseEntity
        {
            string tableName = typeof(TEntity).GetCustomAttribute<EntityMetadataAttribute>().Name;

            return Database.GetCollection<TEntity>(tableName);
        }
    }
}
