using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Files.UploadFile
{
	internal class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Guid>
	{
		private readonly IWriteDbContext _db;
		private readonly IFileReader _fileReader;

		public UploadFileCommandHandler(IWriteDbContext db, IFileReader fileReader)
		{
			_db = db;
			_fileReader = fileReader;
		}

		public async Task<Guid> HandleAsync(UploadFileCommand request, CancellationToken cancellationToken)
		{
			File file = await _fileReader.ReadAsync(request.File, cancellationToken);

			await _db.Files.CreateAsync(file);

			return file.Id;
		}
	}
}
