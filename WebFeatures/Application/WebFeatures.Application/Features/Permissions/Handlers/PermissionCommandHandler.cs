using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Permissions.Requests;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Domian.Entities.Permissions;

namespace WebFeatures.Application.Features.Permissions.Handlers
{
    internal class PermissionCommandHandler : IRequestHandler<UserHasPermission, bool>
    {
        private readonly IWriteDbContext _db;
        private readonly ICurrentUserService _currentUser;

        public PermissionCommandHandler(IWriteDbContext db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<bool> HandleAsync(UserHasPermission request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated) return false;

            IEnumerable<RolePermission> userPermissions = await _db.RolePermissions.GetByUserIdAsync(_currentUser.UserId);

            return userPermissions.Any(x => x.Permission == request.Permission);
        }
    }
}