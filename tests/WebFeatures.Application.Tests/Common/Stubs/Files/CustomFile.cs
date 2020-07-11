using System.IO;
using System.Net.Mime;
using Bogus;
using WebFeatures.Application.Interfaces.Files;

namespace WebFeatures.Application.Tests.Common.Stubs.Files
{
	internal class CustomFile : IFile
	{
		private static readonly string[] ContentTypes =
		{
			MediaTypeNames.Image.Jpeg,
			MediaTypeNames.Application.Json
		};

		public CustomFile()
		{
			Faker faker = new Faker();

			Name = faker.System.CommonFileName();
			ContentType = faker.PickRandom(ContentTypes);
			Content = faker.Random.Bytes(16);
		}

		public byte[] Content { get; }
		public string Name { get; private set; }
		public string ContentType { get; }

		public Stream OpenReadStream()
		{
			return new MemoryStream(Content);
		}

		public CustomFile WithExtension(string extension)
		{
			Name = Path.GetFileNameWithoutExtension(Name) + "." + extension;

			return this;
		}
	}
}
