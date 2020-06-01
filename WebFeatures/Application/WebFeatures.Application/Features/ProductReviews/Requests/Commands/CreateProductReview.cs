using AutoMapper;
using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Features.ProductReviews.Requests.Commands
{
    /// <summary>
    /// Создать обзор на товар
    /// </summary>
    public class CreateProductReview : ICommand<Guid>, IHasMappings
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
            profile.CreateMap<CreateProductReview, ProductReview>(MemberList.Source);
        }

        public class Validator : AbstractValidator<CreateProductReview>
        {
            public Validator(IWriteDbContext db)
            {
                RuleFor(r => r.ProductId)
                    .MustAsync(async (x, t) => await db.Products.ExistsAsync(x));

                RuleFor(r => r.Title)
                    .NotEmpty();

                RuleFor(r => r.Comment)
                    .NotEmpty();
            }
        }
    }
}