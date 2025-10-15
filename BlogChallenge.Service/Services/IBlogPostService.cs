using BlogChallenge.Domain.Models;

namespace BlogChallenge.Service.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost>> GetAllPosts();
        Task<BlogPost> GetPostById(Guid id);
        Task<BlogPost> CreateBlogPost(BlogPost blogPost);
        Task AddCommentToPost(Guid blogPostId, Comment comment);
    }
}
