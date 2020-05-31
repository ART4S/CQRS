using Microsoft.Extensions.DependencyInjection;
using WebFeatures.Requests.Internal;

namespace WebFeatures.Requests.DependencyInjection
{
    public static class ConfigureServices
    {
        public static void AddRequestMediator(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            services.Add(new ServiceDescriptor(
                typeof(IRequestMediator),
                typeof(RequestMediator),
                lifetime));
        }
    }
}
