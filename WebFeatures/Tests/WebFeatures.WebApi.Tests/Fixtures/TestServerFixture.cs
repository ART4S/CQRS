using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace WebFeatures.WebApi.Tests.Fixtures
{
    public class TestServerFixture
    {
        public TestServer Server { get; }

        public TestServerFixture()
        {
            Server = new TestServer(
                new WebHostBuilder()
                .ConfigureAppConfiguration((ctx, config) =>
                {
                    config.AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json");
                })
                .UseStartup<Startup>()
                .UseEnvironment("Testing"));
        }
    }
}