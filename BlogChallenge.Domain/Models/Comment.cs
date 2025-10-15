namespace BlogChallenge.Domain.Models
{
    public class Comment
    {
        public string Content { get; set; }
        
        public Comment(string content)
        {
            Content = content;
        }
    }
}
