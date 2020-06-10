using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Writing.Repositories;
using WebFeatures.Domian.Entities.Permissions;

namespace WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing
{
    public interface IRolePermissionWriteRepository : IWriteRepository<RolePermission>
    {
        Task<IEnumerable<RolePermission>> GetByUserIdAsync(Guid userId);
    }
}
