namespace Interngram.Repository.Models;

public class Post
{
    public string Id { get; set; } = null!;
    public string AuthorId { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public List<string> Likes { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}