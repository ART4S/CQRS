using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebFeatures.WebApi
{
    public class Program
    {
        public async static void Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) 
            => WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
