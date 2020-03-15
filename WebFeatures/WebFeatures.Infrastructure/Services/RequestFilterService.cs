using Microsoft.AspNetCore.Http;
using System.Web;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Infrastructure.Services
{
    internal class RequestFilterService : IRequestFilterService
    {
        public string Filter { get; }

        public RequestFilterService(IHttpContextAccessor contextAccessor)
        {
            Filter = HttpUtility.UrlDecode(contextAccessor.HttpContext.Request.QueryString.Value);
        }
    }
}
