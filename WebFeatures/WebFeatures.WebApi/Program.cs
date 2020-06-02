using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using WebFeatures.WebApi.Extensions;

namespace WebFeatures.WebApi
{
    public class Program
    {
        public static Task Main(string[] args)
            => CreateWebHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateWebHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder
                        .UseStartup<Startup>()
                        .UseKestrel(options =>
                        {
                            options.Limits.MaxRequestBodySize = 50 * 1024 * 1024; // 50 MB
                        });
                })
                .UseLogging();
    }
}
