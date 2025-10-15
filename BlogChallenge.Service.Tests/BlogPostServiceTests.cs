using BlogChallenge.DAL.Interfaces;
using BlogChallenge.Domain.Models;
using BlogChallenge.Service.Services;
using Moq;
using Xunit;

namespace BlogChallenge.Service.Tests
{
    public class BlogPostServiceTests
    {
        public Mock<IBlogPostRepository> _blogPostRepositoryMock { get; set; }
        public BlogPostService _service { get; set; }

        public BlogPostServiceTests()
        {
            _blogPostRepositoryMock = new Mock<IBlogPostRepository>();

            _service = new BlogPostService(_blogPostRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllPosts_NoData_EmptyResults()
        {
            int expectedCount = 0;

            _blogPostRepositoryMock
                .Setup(c => c.GetAllPosts())
                .ReturnsAsync(new List<BlogPost>());

            var result = await _service.GetAllPosts();

            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count());

            _blogPostRepositoryMock.Verify(n => n.GetAllPosts(), Times.Once);
        }

        [Fact]
        public async Task GetAllPosts_HappyPath()
        {
            int expectedCount = 2;

            _blogPostRepositoryMock
                .Setup(c => c.GetAllPosts())
                .ReturnsAsync(GetSampleBlogPosts());

            var result = await _service.GetAllPosts();

            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count());

            Assert.Equal("Content 1", result.First().Content);
            Assert.Equal("Title 1", result.First().Title);

            _blogPostRepositoryMock.Verify(n => n.GetAllPosts(), Times.Once);
        }

        [Theory]
        [InlineData("Title", null, "content")]
        [InlineData("Content", "title", null)]
        public async Task CreateBlogPost_WhenTitleIsNull_ShouldThrownException(string prop, string titleValue, string contentValue)
        {
            var exMessage = $"{prop} cannot be null (Parameter '{prop}')";

            var blogPostRequest = new BlogPost(
                Guid.NewGuid(),
                titleValue,
                contentValue);

            Func<Task> action = async () =>
            {
                await _service.CreateBlogPost(blogPostRequest);
            };
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);

            Assert.NotNull(ex);
            Assert.Equal(exMessage, ex.Message);
            Assert.IsType<ArgumentNullException>(ex);

            _blogPostRepositoryMock.Verify(n => n.AddBlogPost(It.IsAny<BlogPost>()), Times.Never);
        }

        [Fact]
        public async Task CreateBlogPost_HappyPath()
        {
            var blogPostRequest = new BlogPost(
                Guid.NewGuid(),
                "title",
                "content");

            _blogPostRepositoryMock
                .Setup(c => c.AddBlogPost(blogPostRequest))
                .ReturnsAsync(blogPostRequest);

            var blogPostCreated = await _service.CreateBlogPost(blogPostRequest);
            
            Assert.NotNull(blogPostCreated);
            Assert.Equal(blogPostRequest.Title, blogPostCreated.Title);
            Assert.Equal(blogPostRequest.Content, blogPostCreated.Content);

            _blogPostRepositoryMock.Verify(n => n.AddBlogPost(It.IsAny<BlogPost>()), Times.Once);
        }

        private List<BlogPost> GetSampleBlogPosts()
        {
            return new List<BlogPost>
            {
                new BlogPost(
                    Guid.NewGuid(),
                    "Title 1",
                    "Content 1"),
                new BlogPost(
                    Guid.NewGuid(),
                    "Title 2",
                    "Content 2")
            };
        }
    }
}
