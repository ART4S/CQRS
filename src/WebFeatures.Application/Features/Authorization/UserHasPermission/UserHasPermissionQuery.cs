using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Authorization.UserHasPermission
{
	/// <summary>
	/// Проверить наличие разрешения у пользователя
	/// </summary>
	public class UserHasPermissionQuery : IQuery<bool>
	{
		/// <summary>
		/// Наименование разрешения
		/// </summary>
		public string Permission { get; set; }
	}
}
