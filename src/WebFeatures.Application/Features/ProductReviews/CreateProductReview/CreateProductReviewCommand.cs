using System;
using AutoMapper;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Domian.Entities.Products;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Features.ProductReviews.CreateProductReview
{
	/// <summary>
	/// Создать обзор на товар
	/// </summary>
	public class CreateProductReviewCommand : ICommand<Guid>, IHasMappings
	{
		/// <summary>
		/// Идентификатор товара
		/// </summary>
		public Guid ProductId { get; set; }

		/// <summary>
		/// Заголовок
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Комментарий
		/// </summary>
		public string Comment { get; set; }

		/// <summary>
		/// Пользовательская оценка
		/// </summary>
		public ProductRating Rating { get; set; }

		public void ApplyMappings(Profile profile)
		{
			profile.CreateMap<CreateProductReviewCommand, ProductReview>(MemberList.Source);
		}
	}
}
