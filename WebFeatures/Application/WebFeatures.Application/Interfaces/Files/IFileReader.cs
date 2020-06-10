using System.Threading.Tasks;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Interfaces.Files
{
    public interface IFileReader
    {
        public Task<File> ReadAsync(IFile file);
    }
}
