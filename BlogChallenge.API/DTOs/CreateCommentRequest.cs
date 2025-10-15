using System.ComponentModel.DataAnnotations;

namespace BlogChallenge.API.DTOs
{
    public class CreateCommentRequest
    {
        [Required(ErrorMessage = "Content is mandatory")]
        [StringLength(50, ErrorMessage = "Content max lenght is 50")]
        public string Content { get; set; }

        public Domain.Models.Comment ToDomain()
        {
            var comment = new Domain.Models.Comment(Content);
            return comment;
        }
    }
}
