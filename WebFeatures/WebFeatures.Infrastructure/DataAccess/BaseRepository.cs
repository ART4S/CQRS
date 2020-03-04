using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Infrastructure.DataAccess
{
    public class BaseRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>, new()
        where TId : struct
    {
        protected WebFeaturesDbContext Context;
        protected readonly IDateTime DateTime;

        public BaseRepository(WebFeaturesDbContext context, IDateTime dateTime)
        {
            Context = context;
            DateTime = dateTime;
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
            var now = DateTime.Now;

            foreach (var entry in Context.ChangeTracker.Entries<IUpdatable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = now;
                        break;
                }
            }

            Context.SaveChanges();
        }
    }
}
