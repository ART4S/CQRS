using Microsoft.AspNetCore.Http;
using System.Web;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Infrastructure.Services
{
    internal class RequestFilterService : IRequestFilterService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public RequestFilterService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetFilter()
        {
            return HttpUtility.UrlDecode(_contextAccessor.HttpContext.Request.QueryString.Value);
        }
    }
}
