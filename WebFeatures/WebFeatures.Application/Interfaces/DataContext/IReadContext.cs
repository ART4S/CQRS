using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebFeatures.Domian.Common;

namespace WebFeatures.Application.Interfaces.DataContext
{
    public interface IReadContext
    {
        Task<IList<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : BaseEntity;
    }
}
