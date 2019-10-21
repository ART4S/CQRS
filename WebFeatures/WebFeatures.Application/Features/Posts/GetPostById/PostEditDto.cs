using System;

namespace WebFeatures.Application.Features.Posts.GetPostById
{
    public class PostEditDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
