using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Features.Reviews.CreateReview
{
    public class CreateReview : ICommand<Guid>, IHasMappings
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public UserRating Rating { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<CreateReview, Review>(MemberList.Source);
        }

        public class Validator : AbstractValidator<CreateReview>
        {
            public Validator(IWriteContext db, ICurrentUserService currentUser)
            {
                RuleFor(x => x.ProductId)
                    .MustAsync(async (productId, token) => await db.Products.FindAsync(productId) != null);

                RuleFor(x => x.ProductId).MustAsync(async (productId, token) => !await db.Reviews
                    .AnyAsync(x => x.ProductId == productId && x.AuthorId == currentUser.UserId))
                .WithMessage("User already reviewed this product");

                RuleFor(x => x.Title)
                    .Must(x => !string.IsNullOrWhiteSpace(x));
                RuleFor(x => x.Comment)
                    .Must(x => !string.IsNullOrWhiteSpace(x));
            }
        }
    }
}
