using AutoMapper;
using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.ProductComments.CreateProductComment
{
    public class CreateProductComment : ICommand<Guid>, IHasMappings
    {
        public Guid ProductId { get; set; }
        public string Body { get; set; }
        public Guid? ParentCommentId { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<CreateProductComment, ProductComment>(MemberList.Source);
        }

        public class Validator : AbstractValidator<CreateProductComment>
        {
            public Validator(IDbContext db)
            {
                RuleFor(x => x.ProductId)
                    .MustAsync(async (x, t) => await db.Products.ExistsAsync(x));

                RuleFor(x => x.Body)
                    .NotEmpty();

                RuleFor(x => x.ParentCommentId)
                    .MustAsync(async (x, t) => await db.ProductComments.ExistsAsync(x.Value))
                    .When(x => x.ParentCommentId.HasValue);
            }
        }
    }
}
