using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebFeatures.Domian.Common;

namespace WebFeatures.Application.Interfaces
{
    public interface IReadContext
    {
        Task<IList<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = default) where TEntity : BaseEntity;

        Task<TEntity> GetByIdAsync<TEntity>(Guid id) where TEntity : BaseEntity;

        void Add<TEntity>(TEntity entity) where TEntity : BaseEntity;

        Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;

        Task UpsertAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}
