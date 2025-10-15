using BlogChallenge.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BlogChallenge.API.DTOs
{
    public class CreatePostRequest
    {
        [Required(ErrorMessage = "Title is mandatory")]
        [StringLength(50, ErrorMessage = "Title max length is 50")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is mandatory")]
        [StringLength(200, ErrorMessage = "Content max length is 200")]
        public string Content { get; set; }

        public BlogPost ToDomain() => new BlogPost(this.Title, this.Content);
    }
}
