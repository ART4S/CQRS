using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Files.DownloadFile
{
	internal class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, FileDownloadDto>
	{
		private readonly IReadDbContext _db;

		public DownloadFileQueryHandler(IReadDbContext db)
		{
			_db = db;
		}

		public async Task<FileDownloadDto> HandleAsync(DownloadFileQuery request, CancellationToken cancellationToken)
		{
			return await _db.Files.GetAsync(request.Id) ?? throw new ValidationException("File doesn't exist");
		}
	}
}
