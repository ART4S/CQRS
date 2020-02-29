using System.Linq;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.DataContext;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Repositories
{
    public class BaseRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>, new()
        where TId : struct
    {
        protected AppContext Context;

        public BaseRepository(AppContext context)
        {
            Context = context;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public virtual TEntity GetById(TId id)
        {
            return Context.Find<TEntity>(id);
        }

        public virtual bool Exists(TId id)
        {
            return Context.Find<TEntity>(id) != null;
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Remove(TId id)
        {
            var entity = new TEntity {Id = id};
            Context.Remove(entity);
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
