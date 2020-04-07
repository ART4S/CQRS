using Microsoft.Extensions.DependencyInjection;

namespace WebFeatures.Events
{
    public static class ConfigureServices
    {
        public static void AddEventMediator(this IServiceCollection services)
        {
            services.AddTransient<IEventMediator, EventMediator>();
        }
    }
}
