using Microsoft.Extensions.DependencyInjection;

namespace WebFeatures.DatabaseInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new CompositionRoot();
            var scripts = root.Services.GetService<ScriptsRunner>();

            scripts.Run();
        }
    }
}
