using Microsoft.Extensions.DependencyInjection;
using System;
using WebFeatures.Requests.Services;

namespace WebFeatures.Requests.DependencyInjection
{
    internal class ServiceFactoryAdapter : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactoryAdapter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type type)
        {
            return _serviceProvider.GetRequiredService(type);
        }
    }
}
