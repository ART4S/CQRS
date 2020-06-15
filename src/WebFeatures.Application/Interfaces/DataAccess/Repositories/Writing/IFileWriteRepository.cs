using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing
{
    public interface IFileWriteRepository : IWriteRepository<File>
    {
        Task<IEnumerable<File>> GetByProductIdAsync(Guid productId);
    }
}
