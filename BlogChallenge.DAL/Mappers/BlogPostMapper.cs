using BlogChallenge.DAL.Entities;
using BlogChallenge.Domain.Models;
using System.Linq;

namespace BlogChallenge.DAL.Mappers
{
    internal static class BlogPostMapper
    {
        internal static BlogPost ToDomain(BlogPostEntity entity)
        {
            if (entity == null) 
                return null;

            var blogPost = new BlogPost(entity.Id, entity.Title, entity.Content);

            foreach (var comment in entity.Comments)
            {
                var comm = new Domain.Models.Comment(comment.Content);

                blogPost.Comments.Add(comm);    
            }

            return blogPost;
        }

        internal static BlogPostEntity ToEntity(BlogPost domain)
        {
            var blogPostEntity = new BlogPostEntity();
            blogPostEntity.Id = domain.Id;
            blogPostEntity.Content = domain.Content;
            blogPostEntity.Title = domain.Title;
            blogPostEntity.Comments = new List<Entities.CommentEntity>();

            foreach (var comment in domain.Comments)
            {
                var comm = new Entities.CommentEntity();
                comm.Content = comment.Content;
                blogPostEntity.Comments.Add(comm);
            }

            return blogPostEntity;
        }
    }
}
