using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace WebFeatures.WebApi.Utils
{
    internal static class FormDataExtensions
    {
        public static async Task<byte[]> LoadBytesAsync(this IFormFile file)
        {
            await using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}
