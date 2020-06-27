namespace WebFeatures.Application.Features.Files.DownloadFile
{
    /// <summary>
    /// Данные файла для загрузки
    /// </summary>
    public class FileDownloadDto
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string CheckSum { get; set; }
        public byte[] Content { get; set; }
    }
}
