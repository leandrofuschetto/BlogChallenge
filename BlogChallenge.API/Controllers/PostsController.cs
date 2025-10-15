using BlogChallenge.API.DTOs;
using BlogChallenge.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogChallenge.API.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public PostsController(IBlogPostService postService)
        {   
            _blogPostService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPostListItemResponse>>> GetAllPosts()
        {
            var blogPosts = await _blogPostService.GetAllPosts();    

            var listPosts = new List<GetPostListItemResponse>();
            foreach (var post in blogPosts)
            {
                listPosts.Add(GetPostListItemResponse.FromDomain(post));
            }

            return Ok(listPosts);
        }

        [HttpPost]
        public async Task<ActionResult<GetPostResponse>> CreateBlogPost(
            [FromBody] CreatePostRequest postCreateRequest)
        {
            var blogPost = postCreateRequest.ToDomain();

            var newBlogPost = await _blogPostService.CreateBlogPost(blogPost);
            var getPostResponse = GetPostResponse.FromDomain(newBlogPost);

            return CreatedAtAction(
                nameof(GetPostById),
                new { id = getPostResponse.Id },
                getPostResponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetPostResponse>> GetPostById(
            [FromRoute] Guid id)
        {
            var blogPost = await _blogPostService.GetPostById(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return Ok(GetPostResponse.FromDomain(blogPost));
        }

        [HttpPost]
        [Route("{id}/comments")]
        public async Task<ActionResult> AddCommentToPost(
            [FromRoute] Guid id,
            [FromBody] CreateCommentRequest createCommentRequest)
        {
            await _blogPostService.AddCommentToPost(id, createCommentRequest.ToDomain());

            return NoContent();
        }
    }
}
