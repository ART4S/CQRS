using FluentValidation;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPostById
{
    public class GetPostByIdQuery : IQuery<PostEditDto>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<GetPostByIdQuery>
        {
            public Validator(IRepository<Post, int> postRepo)
            {
                RuleFor(x => x.Id)
                    .Must(postRepo.Exists)
                    .WithMessage(ValidationErrorMessages.MissingInDataBase(typeof(Post)));
            }
        }
    }
}
