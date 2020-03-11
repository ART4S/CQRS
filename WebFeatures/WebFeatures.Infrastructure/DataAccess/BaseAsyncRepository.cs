using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Common;
using WebFeatures.DataContext;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Infrastructure.DataAccess
{
    public class BaseAsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        protected WebFeaturesDbContext Context;
        protected readonly IDateTime DateTime;

        public BaseAsyncRepository(WebFeaturesDbContext context, IDateTime dateTime)
        {
            Context = context;
            DateTime = dateTime;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync();
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id) != null;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity == null) 
                return;

            if (entity is ISoftDelete soft)
            {
                soft.IsDeleted = true;
                return;
            }

            Context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task SaveChangesAsync()
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

            await Context.SaveChangesAsync();
        }
    }
}
