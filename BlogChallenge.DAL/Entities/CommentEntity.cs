using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogChallenge.DAL.Entities
{
    public class CommentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string Content { get; set; }

        public Guid BlogPostId { get; set; }

        [ForeignKey("BlogPostId")]
        public BlogPostEntity BlogPost { get; set; }
    }
}
