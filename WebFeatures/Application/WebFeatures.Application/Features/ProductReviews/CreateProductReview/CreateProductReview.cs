using AutoMapper;
using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Features.ProductReviews.CreateProductReview
{
    public class CreateProductReview : ICommand<Guid>, IHasMappings
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public ProductRating Rating { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<CreateProductReview, ProductReview>(MemberList.Source);
        }

        public class Validator : AbstractValidator<CreateProductReview>
        {
            public Validator(IDbContext db)
            {
                RuleFor(x => x.ProductId)
                    .MustAsync(async (x, t) => await db.Products.ExistsAsync(x));
                RuleFor(x => x.Title)
                    .Must(x => !string.IsNullOrWhiteSpace(x));
                RuleFor(x => x.Comment)
                    .Must(x => !string.IsNullOrWhiteSpace(x));
            }
        }
    }
}
