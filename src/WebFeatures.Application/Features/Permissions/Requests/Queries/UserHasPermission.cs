using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Permissions.Requests.Queries
{
    /// <summary>
    /// Проверить наличие разрешения у пользователя
    /// </summary>
    public class UserHasPermission : IQuery<bool>
    {
        public string Permission { get; set; }
    }
}