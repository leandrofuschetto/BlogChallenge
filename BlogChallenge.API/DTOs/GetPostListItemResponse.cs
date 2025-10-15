using BlogChallenge.Domain.Models;

namespace BlogChallenge.API.DTOs
{
    public class GetPostListItemResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int CommentsCount { get; set; }

        public static GetPostListItemResponse FromDomain(BlogPost blogPost)
        {
            var response = new GetPostListItemResponse();
            response.Id = blogPost.Id;
            response.Title = blogPost.Title;
            response.CommentsCount = blogPost.Comments.Count;

            return response;
        }
    }
}


