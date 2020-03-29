using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebFeatures.WebApi.Tests
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpResponseMessage response)
        {
            try
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch
            {
                return default;
            }
        }
    }
}
