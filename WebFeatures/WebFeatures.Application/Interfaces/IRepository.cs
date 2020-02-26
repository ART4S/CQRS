using System.Collections.Generic;
using System.Linq;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Application.Interfaces.Data
{
    public interface IRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId> 
        where TId : struct
    {
        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> GetAllAsQuery();

        bool Exists(TId id);

        void Add(TEntity entity);

        void Remove(TId id);

        void SaveChanges();
    }
}
