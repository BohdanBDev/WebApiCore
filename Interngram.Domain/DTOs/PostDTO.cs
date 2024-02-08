namespace Interngram.Domain.DTOs;

public class PostDTO
{
    public string Id { get; set; } = null!;
    public string AuthorId { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public List<string> Likes { get; set; } = new();
    public List<CommentDTO> Comments { get; set; } = new();
}