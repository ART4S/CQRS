using System;
using System.Collections.Generic;
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

        public T GetService<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }

        public IEnumerable<T> GetServices<T>()
        {
            return (IEnumerable<T>)_serviceProvider.GetService(typeof(IEnumerable<T>));
        }
    }
}
