using System;
using System.Linq;
using System.Reflection;

namespace WebFeatures.Common.Extensions
{
    /// <summary>
    /// Расширения для работы с reflection
    /// </summary>
    public static class ReflectionExtensions
    {
        public static Assembly GetAssembly(this AppDomain domian, string assemblyName)
        {
            var assembly = domian
                .GetAssemblies()
                .FirstOrDefault(x => x.GetName().Name == assemblyName);

            return assembly;
        }
    }
}
