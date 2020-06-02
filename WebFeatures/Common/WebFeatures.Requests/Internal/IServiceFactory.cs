using System;
using System.Collections.Generic;

namespace WebFeatures.Requests.Services
{
    internal interface IServiceFactory
    {
        object GetService(Type type);
    }

    internal static class ServiceFactoryExtensions
    {
        public static T GetService<T>(this IServiceFactory serviceFactory)
        {
            return (T)serviceFactory.GetService(typeof(T));
        }

        public static IEnumerable<T> GetServices<T>(this IServiceFactory serviceFactory)
        {
            return (IEnumerable<T>)serviceFactory.GetService(typeof(IEnumerable<T>));
        }
    }
}