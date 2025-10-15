using BlogChallenge.Domain.Models;

namespace BlogChallenge.API.DTOs
{
    public class GetPostResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<CommentsResponse> Comments { get; set; }

        public static GetPostResponse FromDomain(BlogPost blogPost)
        {
            GetPostResponse GetAllPostResponse = new();
            GetAllPostResponse.Id = blogPost.Id;
            GetAllPostResponse.Title = blogPost.Title;
            GetAllPostResponse.Content = blogPost.Content;
            GetAllPostResponse.Comments = blogPost.Comments.Select(c => new CommentsResponse
            {
                Content = c.Content
            }).ToList();

            return GetAllPostResponse;
        }
    }

    public class CommentsResponse
    { 
        public string Content { get; set; }
    }
}
