using System.IO;

namespace WebFeatures.Application.Helpers
{
    internal static class StreamExtensions
    {
        public static byte[] ReadBytes(this Stream stream)
        {
            using var br = new BinaryReader(stream);
            return br.ReadBytes((int)stream.Length);
        }
    }
}
