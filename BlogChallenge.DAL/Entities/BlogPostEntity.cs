using BlogChallenge.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogChallenge.DAL.Entities
{
    public class BlogPostEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(200)")]
        public string Content { get; set; }

        public virtual List<CommentEntity> Comments { get; set; }

        public BlogPostEntity()
        {
            Comments = new List<CommentEntity>();
        }
    }
}
