using System.Threading.Tasks;

namespace WebFeatures.Application.Infrastructure.Files
{
    public interface IFile
    {
        string Name { get; }
        string ContentType { get; }

        Task<byte[]> ReadBytesAsync();
    }
}
