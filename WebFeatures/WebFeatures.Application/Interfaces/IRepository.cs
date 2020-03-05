using System;
using System.Linq;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(Guid id);

        bool Exists(Guid id);

        void Add(TEntity entity);

        void Remove(Guid id);

        void SaveChanges();
    }
}
