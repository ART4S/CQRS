using System;
using System.Collections.Generic;

namespace WebFeatures.Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        Guid UserId { get; }
        IEnumerable<string> Roles { get; }
    }
}
