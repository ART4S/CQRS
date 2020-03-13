using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace WebFeatures.Requests
{
    public static class DependencyInjection
    {
        public static void AddRequests(this IServiceCollection services, Assembly assemblyToScan)
        {
            services.AddTransient<IRequestMediator, RequestMediator>();

            foreach (var type in assemblyToScan.GetExportedTypes())
            {
                Type interfaceType = type.GetInterfaces()
                    .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

                if (interfaceType != null)
                {
                    services.AddTransient(interfaceType, type);
                }
            }
        }
    }
}
