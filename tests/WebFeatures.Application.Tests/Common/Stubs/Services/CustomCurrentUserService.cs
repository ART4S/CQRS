using System;
using WebFeatures.Application.Interfaces.Services;

namespace WebFeatures.Application.Tests.Common.Stubs.Services
{
    internal class CustomCurrentUserService : ICurrentUserService
    {
        public Guid UserId { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}