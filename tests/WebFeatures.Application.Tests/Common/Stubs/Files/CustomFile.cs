using Bogus;
using System.IO;
using System.Net.Mime;
using WebFeatures.Application.Interfaces.Files;

namespace WebFeatures.Application.Tests.Common.Stubs.Files
{
    internal class CustomFile : IFile
    {
        public string Name { get; private set; }

        public string ContentType { get; }

        public byte[] Content { get; }

        public Stream OpenReadStream() => new MemoryStream(Content);

        private static readonly string[] ContentTypes =
        {
            MediaTypeNames.Image.Jpeg,
            MediaTypeNames.Application.Json
        };

        public CustomFile()
        {
            var faker = new Faker();

            Name = faker.System.CommonFileName();
            ContentType = faker.PickRandom(ContentTypes);
            Content = faker.Random.Bytes(16);
        }

        public CustomFile WithExtension(string extension)
        {
            Name = Path.GetFileNameWithoutExtension(Name) + "." + extension;

            return this;
        }
    }
}