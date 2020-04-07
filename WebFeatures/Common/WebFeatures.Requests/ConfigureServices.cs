using Microsoft.Extensions.DependencyInjection;
using WebFeatures.Requests.Internal;

namespace WebFeatures.Requests
{
    public static class ConfigureServices
    {
        public static void AddRequestMediator(this IServiceCollection services)
        {
            services.AddScoped<IRequestMediator, RequestMediator>();
        }
    }
}
