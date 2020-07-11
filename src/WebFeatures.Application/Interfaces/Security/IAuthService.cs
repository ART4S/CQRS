using System;
using System.Threading.Tasks;

namespace WebFeatures.Application.Interfaces.Security
{
	public interface IAuthService
	{
		Task<bool> UserHasPermissionAsync(Guid userId, string permission);
	}
}
