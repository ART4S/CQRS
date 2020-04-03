using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataContext;

namespace WebFeatures.Application.Features.Comments.CreateComment
{
    public class CreateComment : ICommand<Guid>
    {
        public Guid ProductId { get; set; }
        public string Body { get; set; }
        public Guid? ParentCommentId { get; set; }

        public class Validator : AbstractValidator<CreateComment>
        {
            public Validator(IWriteContext db)
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
