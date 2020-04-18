using AutoMapper;
using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Comments.CreateComment
{
    public class CreateComment : ICommand<Guid>, IHasMappings
    {
        public Guid ProductId { get; set; }
        public string Body { get; set; }
        public Guid? ParentCommentId { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<CreateComment, UserComment>(MemberList.Source);
        }

        public class Validator : AbstractValidator<CreateComment>
        {
            public Validator(IDbContext db)
            {
                RuleFor(x => x.ProductId)
                    .MustAsync(async (x, t) => await db.Products.FindAsync(x) != null);

                RuleFor(x => x.Body)
                    .NotEmpty();

                RuleFor(x => x.ParentCommentId)
                    .MustAsync(async (x, t) => await db.UserComments.FindAsync(x) != null)
                    .When(x => x.ParentCommentId.HasValue);
            }
        }
    }
}
