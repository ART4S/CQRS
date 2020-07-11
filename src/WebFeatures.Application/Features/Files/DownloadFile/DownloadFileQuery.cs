using System;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Files.DownloadFile
{
	/// <summary>
	/// Получить файл
	/// </summary>
	public class DownloadFileQuery : IQuery<FileDownloadDto>
	{
		/// <summary>
		/// Идентификатор файла
		/// </summary>
		public Guid Id { get; set; }
	}
}
