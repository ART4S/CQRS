using Microsoft.Extensions.DependencyInjection;

namespace WebFeatures.AppInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new CompositionRoot();
            var scripts = root.Services.GetService<ScriptsExecutor>();
            scripts.Execute();
        }
    }
}
