using BlogChallenge.DAL.Interfaces;
using BlogChallenge.Domain.Exceptions;
using BlogChallenge.Domain.Models;
using BlogChallenge.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BlogChallenge.Service.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository  _blogPostRepository;
        
        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public Task<IEnumerable<BlogPost>> GetAllPosts() 
            => _blogPostRepository.GetAllPosts();

        public async Task<BlogPost> CreateBlogPost(BlogPost blogPost)
        {
            ValidateBlogPostCreateRequest(blogPost);

            var blogPostCreated =  await _blogPostRepository.AddBlogPost(blogPost);

            return blogPostCreated;
        }

        public async Task<BlogPost> GetPostById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Id cannot be empty");

            return await _blogPostRepository.GetPostById(id);
        }

        public async Task AddCommentToPost(Guid blogPostId, Comment comment)
        {
            ValidateAddComment(blogPostId, comment);
                
            var post = await _blogPostRepository.GetPostById(blogPostId);

            if (post == null)
                throw new EntityNotFoundException(
                    $"Post with id: {blogPostId} not found", 
                    "POST_NOT_FOUND");

            await _blogPostRepository.UpdatePostWithComments(post, comment);
        }

        private void ValidateBlogPostCreateRequest(BlogPost blogPost)
        {
            if (string.IsNullOrWhiteSpace(blogPost.Title))
                throw new ArgumentNullException("Title", "Title cannot be null");

            if (string.IsNullOrWhiteSpace(blogPost.Content))
                throw new ArgumentNullException("Content", "Content cannot be null");
        }

        private void ValidateAddComment(Guid blogPostId, Comment comment)
        {
            if (blogPostId == Guid.Empty)
                throw new ArgumentNullException("Id cannot be empty");

            if (comment == null || string.IsNullOrWhiteSpace(comment.Content))
                throw new ArgumentNullException("Comment", "Comment cannot be null");
        }
    }
}
