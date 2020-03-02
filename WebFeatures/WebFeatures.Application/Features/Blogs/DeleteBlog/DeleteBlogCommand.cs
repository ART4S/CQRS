using FluentValidation;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.DeleteBlog
{
    public class DeleteBlogCommand : ICommand<Unit>
    {
        public int Id { get; set; }

        public class Validator : AbstractValidator<DeleteBlogCommand>
        {
            public Validator(IRepository<Blog, int> blogRepo)
            {
                RuleFor(x => x.Id)
                    .Must(blogRepo.Exists)
                    .WithMessage(ValidationErrorMessages.MissingInDataBase(typeof(Blog)));
            }
        }
    }
}
