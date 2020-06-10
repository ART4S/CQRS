using System.IO;

namespace WebFeatures.Application.Interfaces.Files
{
    public interface IFile
    {
        string Name { get; }
        string ContentType { get; }
        Stream OpenReadStream();
    }
}
