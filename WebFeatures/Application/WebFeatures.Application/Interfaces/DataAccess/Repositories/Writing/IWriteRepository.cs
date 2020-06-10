using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Domian.Common;

namespace WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories
{
    public interface IWriteRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Guid id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(IEnumerable<TEntity> entities);
        Task<bool> ExistsAsync(Guid id);
    }
}