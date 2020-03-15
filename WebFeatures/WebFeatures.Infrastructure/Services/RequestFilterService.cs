using System;
using Microsoft.AspNetCore.Http;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Infrastructure.Services
{
    internal class RequestFilterService : IRequestFilterService
    {
        public string Filter { get; }

        public RequestFilterService(IHttpContextAccessor contextAccessor)
        {
            Filter = contextAccessor.HttpContext.Request.QueryString.Value;
            throw new NotImplementedException("Replace space symbols");
        }
    }
}
