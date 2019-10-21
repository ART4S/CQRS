using AutoMapper;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.CreatePost
{
    public class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, Unit>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Unit Handle(CreatePostCommand input)
        {
            var post = _mapper.Map<Post>(input);
            _context.Add(post);

            return Unit.Value;
        }
    }
}
