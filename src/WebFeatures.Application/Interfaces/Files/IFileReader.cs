using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Interfaces.Files
{
	public interface IFileReader
	{
		Task<File> ReadAsync(IFile file, CancellationToken cancellationToken);
	}
}
