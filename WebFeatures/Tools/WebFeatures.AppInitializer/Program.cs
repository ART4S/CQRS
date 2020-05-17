using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebFeatures.AppInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new CompositionRoot();

            var scripts = root.ServiceProvider.GetService<ScriptsExecutor>();

            scripts.Execute();
        }
    }
}
