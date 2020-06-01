using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Domian.Common;

namespace WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories
{
    public interface IWriteRepository<TEntity> where TEntity : IdentityEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Guid id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<bool> ExistsAsync(Guid id);
    }
}