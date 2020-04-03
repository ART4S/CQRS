using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace WebFeatures.Events
{
    public static class DependencyInjection
    {
        public static void AddEvents(this IServiceCollection services, Assembly assemblyToScan)
        {
            services.AddTransient<IEventMediator, EventMediator>();

            foreach (Type type in assemblyToScan.GetExportedTypes())
            {
                Type interfaceType = type.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>));

                if (interfaceType != null)
                {
                    services.AddTransient(interfaceType, type);
                }
            }
        }
    }
}
