using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Features.Files.DownloadFile;
using WebFeatures.Application.Features.Files.UploadFile;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.WebApi.Attributes;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
	/// <summary>
	/// Работа с файлами
	/// </summary>
	public class FilesController : BaseController
	{
		/// <summary>
		/// Получить файл
		/// </summary>
		/// <param name="id">Идентификатор файла</param>
		/// <returns>Файл</returns>
		/// <response code="200">Успех</response>
		/// <response code="400" cref="ValidationError">Ошибка валидации</response>
		[HttpGet("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Download(Guid id)
		{
			FileDownloadDto file = await Mediator.SendAsync(new DownloadFileQuery { Id = id });

			return File(file.Content, file.ContentType, file.Name);
		}

		/// <summary>
		/// Создать файл
		/// </summary>
		/// <returns>Идентификатор созданного файла</returns>
		/// <response code="201" cref="Guid">Успех</response>
		/// <response code="403">Доступ запрещен</response>
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AuthorizePermission(PermissionConstants.Files.Upload)]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> Upload([FromForm] IFile file)
		{
			return Created(await Mediator.SendAsync(new UploadFileCommand { File = file }));
		}
	}
}
