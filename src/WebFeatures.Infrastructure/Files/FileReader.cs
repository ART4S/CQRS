using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Files;
using File = WebFeatures.Domian.Entities.File;

namespace WebFeatures.Infrastructure.Files
{
    internal class FileReader : IFileReader
    {
        public async Task<File> ReadAsync(IFile file, CancellationToken cancellationToken)
        {
            byte[] content;

            await using (MemoryStream ms = new MemoryStream())
            {
                await using (Stream rs = file.OpenReadStream())
                {
                    await rs.CopyToAsync(ms, cancellationToken);
                }

                content = ms.ToArray();
            }

            string checkSum = CalculateCheckSum(content);

            return new File()
            {
                Name = file.Name,
                ContentType = file.ContentType,
                Content = content,
                CheckSum = checkSum
            };
        }

        private string CalculateCheckSum(byte[] content)
        {
            using SHA256 hasher = SHA256.Create();

            byte[] hash = hasher.ComputeHash(content);

            return Convert.ToBase64String(hash);
        }
    }
}