using System.Collections.Generic;

namespace WebFeatures.Requests.Services
{
    internal interface IServiceFactory
    {
        T GetService<T>();
        IEnumerable<T> GetServices<T>();
    }
}
