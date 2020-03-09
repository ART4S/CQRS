using System.Collections.Generic;

namespace WebFeatures.Application.Interfaces
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        string UserId { get; }
        IEnumerable<string> Roles { get; }
    }
}
