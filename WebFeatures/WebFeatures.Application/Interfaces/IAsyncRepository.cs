using System;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Application.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        // Leaky abstraction...But who cares ¯\_(ツ)_/¯
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task RemoveAsync(Guid id);
        Task SaveChangesAsync();
    }
}
