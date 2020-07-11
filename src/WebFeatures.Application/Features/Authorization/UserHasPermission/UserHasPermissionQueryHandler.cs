using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Interfaces.Services;

namespace WebFeatures.Application.Features.Authorization.UserHasPermission
{
	internal class UserHasPermissionQueryHandler : IRequestHandler<UserHasPermissionQuery, bool>
	{
		private readonly IAuthService _authService;
		private readonly ICurrentUserService _currentUser;

		public UserHasPermissionQueryHandler(IAuthService authService, ICurrentUserService currentUser)
		{
			_authService = authService;
			_currentUser = currentUser;
		}

		public async Task<bool> HandleAsync(UserHasPermissionQuery request, CancellationToken cancellationToken)
		{
			return _currentUser.IsAuthenticated&& await _authService.UserHasPermissionAsync(_currentUser.UserId, request.Permission);
		}
	}
}
