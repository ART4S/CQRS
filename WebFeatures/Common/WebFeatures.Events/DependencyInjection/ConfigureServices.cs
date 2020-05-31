using Microsoft.Extensions.DependencyInjection;

namespace WebFeatures.Events.DependencyInjection
{
    public static class ConfigureServices
    {
        public static void AddEventMediator(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            services.Add(new ServiceDescriptor(
                typeof(IEventMediator),
                typeof(EventMediator),
                lifetime));
        }
    }
}
