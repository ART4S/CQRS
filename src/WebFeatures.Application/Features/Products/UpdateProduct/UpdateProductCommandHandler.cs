using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Features.Products.UpdateProduct
{
	internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Empty>
	{
		private readonly IWriteDbContext _db;
		private readonly IEventMediator _events;
		private readonly IFileReader _fileReader;
		private readonly IMapper _mapper;

		public UpdateProductCommandHandler(
			IWriteDbContext db,
			IEventMediator events,
			IFileReader fileReader,
			IMapper mapper)
		{
			_db = db;
			_events = events;
			_fileReader = fileReader;
			_mapper = mapper;
		}

		public async Task<Empty> HandleAsync(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			Product product = await _db.Products.GetAsync(request.Id);

			_mapper.Map(request, product);

			if (request.MainPicture != null)
			{
				File newPicture = await _fileReader.ReadAsync(request.MainPicture, cancellationToken);

				if (product.MainPictureId.HasValue)
				{
					File oldPicture = await _db.Files.GetAsync(product.MainPictureId.Value);

					if (!string.Equals(oldPicture.CheckSum, newPicture.CheckSum, StringComparison.OrdinalIgnoreCase))
					{
						await _db.Files.DeleteAsync(oldPicture);
						await _db.Files.CreateAsync(newPicture);

						product.MainPictureId = newPicture.Id;
					}
				}
				else
				{
					await _db.Files.CreateAsync(newPicture);

					product.MainPictureId = newPicture.Id;
				}
			}

			await _db.Products.UpdateAsync(product);

			File[] actualPictures = await Task.WhenAll(
				request.Pictures.Select(x => _fileReader.ReadAsync(x, cancellationToken)));

			HashSet<File> oldPictures = (await _db.Files.GetByProductIdAsync(product.Id)).ToHashSet();

			IEnumerable<File> newPictures = actualPictures.Where(n => oldPictures.All(
				o => !string.Equals(n.CheckSum, o.CheckSum, StringComparison.OrdinalIgnoreCase)));

			foreach (File picture in newPictures)
			{
				await _db.Files.CreateAsync(picture);

				await _db.ProductPictures.CreateAsync(new ProductPicture
				{
					ProductId = product.Id,
					FileId = picture.Id
				});
			}

			await _db.Files.DeleteAsync(oldPictures);

			await _events.PublishAsync(new ProductUpdated(product.Id), cancellationToken);

			return Empty.Value;
		}
	}
}
