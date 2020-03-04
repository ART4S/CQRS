using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WebFeatures.WebApi.Areas.Identity.IdentityHostingStartup))]
namespace WebFeatures.WebApi.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}