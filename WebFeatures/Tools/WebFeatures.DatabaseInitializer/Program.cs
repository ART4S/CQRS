using Microsoft.Extensions.DependencyInjection;

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
