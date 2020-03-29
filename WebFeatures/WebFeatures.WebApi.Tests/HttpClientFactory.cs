using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebFeatures.WebApi.Tests
{
    internal static class HttpClientFactory
    {
        public static async Task<HttpClient> Create()
        {
            var server = new TestServer(
                new WebHostBuilder()
                .UseStartup<Startup>());

            await TestData.SeedDataAsync(server.Services);

            return server.CreateClient();
        }
    }
}