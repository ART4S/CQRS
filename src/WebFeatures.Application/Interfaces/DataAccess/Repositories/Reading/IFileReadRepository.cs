using System;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Files.DownloadFile;

namespace WebFeatures.Application.Interfaces.DataAccess.Repositories.Reading
{
	public interface IFileReadRepository
	{
		Task<FileDownloadDto> GetAsync(Guid id);
	}
}
