using System;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Files.UploadFile
{
    /// <summary>
    /// Загрузить файл
    /// </summary>
    public class UploadFileCommand : ICommand<Guid>
    {
        /// <summary>
        /// Файл
        /// </summary>
        public IFile File { get; set; }
    }
}