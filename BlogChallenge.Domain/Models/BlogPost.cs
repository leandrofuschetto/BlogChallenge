namespace BlogChallenge.Domain.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

        public BlogPost(
            string title, 
            string content)
        {
            Title = title;
            Content = content;
        }

        public BlogPost(
            Guid rid,
            string title,
            string content)
            : this(title, content)
        {
            Id = rid;
        }
    }
}
