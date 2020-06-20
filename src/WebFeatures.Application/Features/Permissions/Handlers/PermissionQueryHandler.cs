using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Permissions.Requests.Queries;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Interfaces.Services;

namespace WebFeatures.Application.Features.Permissions.Handlers
{
    internal class PermissionQueryHandler : IRequestHandler<UserHasPermission, bool>
    {
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUser;

        public PermissionQueryHandler(IAuthService authService, ICurrentUserService currentUser)
        {
            _authService = authService;
            _currentUser = currentUser;
        }

        public async Task<bool> HandleAsync(UserHasPermission request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated) return false;

            return await _authService.UserHasPermissionAsync(_currentUser.UserId, request.Permission);
        }
    }
}