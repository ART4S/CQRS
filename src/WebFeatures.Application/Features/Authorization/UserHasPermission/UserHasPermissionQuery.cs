using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Authorization.UserHasPermission
{
    /// <summary>
    /// Проверить наличие разрешения у пользователя
    /// </summary>
    public class UserHasPermissionQuery : IQuery<bool>
    {
        public string Permission { get; set; }
    }
}