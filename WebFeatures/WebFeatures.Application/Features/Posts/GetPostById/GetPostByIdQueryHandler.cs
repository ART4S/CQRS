using AutoMapper;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Posts.GetPostById
{
    public class GetPostByIdQueryHandler : IQueryHandler<GetPostByIdQuery, PostEditDto>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PostEditDto Handle(GetPostByIdQuery input)
        {
            var post = _context.GetById<Post>(input.Id);
            return _mapper.Map<PostEditDto>(post);
        }
    }
}
