using System;
using System.Collections.Generic;

namespace WebFeatures.Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        IEnumerable<string> Roles { get; }
    }
}
