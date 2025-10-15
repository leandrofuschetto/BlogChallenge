using BlogChallenge.DAL.Entities;
using BlogChallenge.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogChallenge.DAL
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public virtual DbSet<BlogPostEntity> Posts { get; set; }
        public virtual DbSet<CommentEntity> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed
            modelBuilder.Entity<BlogPostEntity>().HasData(
                new BlogPostEntity
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "First Post",
                    Content = "with Content"
                },
                new BlogPostEntity 
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "Second Post",
                    Content = "with another Content"
                }
            );

            // Seed comments
            modelBuilder.Entity<CommentEntity>().HasData(
                new CommentEntity
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    BlogPostId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Content = "First Comment"
                }
            );
        }
    }
}
