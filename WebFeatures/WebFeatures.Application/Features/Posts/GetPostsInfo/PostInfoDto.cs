using System;

namespace WebFeatures.Application.Features.Posts.GetPostsInfo
{
    public class PostInfoDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int BlogId { get; set; }
        public string BlogAuthorName { get; set; }
        public string BlogTitle { get; set; }
        public string Title { get; set; }
    }
}
