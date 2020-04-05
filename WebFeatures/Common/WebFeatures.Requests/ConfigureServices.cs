using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using WebFeatures.Requests.Internal;

namespace WebFeatures.Requests
{
    public static class ConfigureServices
    {
        public static void AddRequests(this IServiceCollection services, Assembly assemblyToScan)
        {
            services.AddScoped<IRequestMediator, RequestMediator>();

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
