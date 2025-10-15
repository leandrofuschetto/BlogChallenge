using BlogChallenge.Domain.Models;

namespace BlogChallenge.DAL.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllPosts();
        Task<BlogPost> AddBlogPost(BlogPost blogPost);
        Task<BlogPost> GetPostById(Guid id);
        Task UpdatePostWithComments(BlogPost blogPost, Comment newComment);
    }
}
