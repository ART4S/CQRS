using AutoMapper;
using FluentValidation;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.CreatePost
{
    public class CreatePostCommand : ICommand<Unit>, IHasMappings
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<CreatePostCommand, Post>(MemberList.Source);
        }

        public class Validator : AbstractValidator<CreatePostCommand>
        {
            public Validator(IRepository<Blog, int> blogRepo)
            {
                RuleFor(x => x.BlogId)
                    .Must(blogRepo.Exists)
                    .WithMessage(b => ValidationErrorMessages.MissingInDataBase(typeof(Blog)));
            }
        }
    }
}
