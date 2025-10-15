using BlogChallenge.DAL.Interfaces;
using BlogChallenge.DAL.Mappers;
using BlogChallenge.Domain.Exceptions;
using BlogChallenge.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;

namespace BlogChallenge.DAL.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogContext _dbContext;
        private readonly ILogger<BlogPostRepository> _logger;
        private readonly string CLASS_NAME = typeof(BlogPostRepository).Name;

        public BlogPostRepository(
            BlogContext blogContext,
            ILogger<BlogPostRepository> logger)
        {
            _dbContext = blogContext;
            _logger = logger;
        }

        public async Task<BlogPost> AddBlogPost(BlogPost blogPost)
        {
            try
            {
                var blogPostEntity = BlogPostMapper.ToEntity(blogPost);

                await _dbContext.Posts.AddAsync(blogPostEntity);

                await _dbContext.SaveChangesAsync();

                return BlogPostMapper.ToDomain(blogPostEntity);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error ocurrs when creating new blogPost. At {CLASS_NAME}, AddBlogPost");

                throw new DataBaseContextException(
                    ex.Message,
                    ex);
            }
        }

        public async Task<IEnumerable<BlogPost>> GetAllPosts()
        {
            try
            {
                var blogPostsEntities = await _dbContext.Posts
                    .Include(p => p.Comments)
                    .ToListAsync();

                var blogPosts = new List<BlogPost>();
                foreach (var post in blogPostsEntities)
                {
                    blogPosts.Add(BlogPostMapper.ToDomain(post));
                }

                return blogPosts;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Invalid Operation Exception ocurrs when getting all posts. At {CLASS_NAME}, GetAllPosts");

                throw new DataBaseContextException(ex.Message, ex);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Timeout ocurrs when getting all posts. At {CLASS_NAME}, GetAllPosts");

                throw new DataBaseContextException(ex.Message, ex);
            }
        }

        public async Task<BlogPost> GetPostById(Guid id)
        {
            try
            {
                var blogPostEntity = await _dbContext.Posts
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(n => n.Id == id);

                return BlogPostMapper.ToDomain(blogPostEntity);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error ocurrs when getting posts by Id. At {CLASS_NAME}, GetPostById, Id: {id}");

                throw new DataBaseContextException(
                    ex.Message,
                    ex);
            }
        }

        public async Task UpdatePostWithComments(BlogPost blogPost, Comment newComment)
        {
            try
            {
                var commentEntity = CommentMapper.ToEntity(newComment);
                commentEntity.BlogPostId = blogPost.Id;

                await _dbContext.Comments.AddAsync(commentEntity);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error ocurrs when adding a commnent to a post. At {CLASS_NAME}, UpdatePostWithComments, Id: {blogPost.Id}");

                throw new DataBaseContextException(
                    ex.Message,
                    ex);
            }
        }
    }
}
