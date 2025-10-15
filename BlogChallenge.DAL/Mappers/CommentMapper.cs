using BlogChallenge.DAL.Entities;
using BlogChallenge.Domain.Models;

namespace BlogChallenge.DAL.Mappers
{
    internal static class CommentMapper
    {
        internal static CommentEntity ToEntity(Comment domain)
        {
            var commentEntity = new CommentEntity();
            commentEntity.Content = domain.Content;
            
            return commentEntity;
        }
    }
}
