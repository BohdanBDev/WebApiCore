using Interngram.Repository.Core.Models;

namespace Interngram.Domain.DTOs;

public class CommentDTO
{
    public string Id { get; set; } = null!;
    public string PostId { get; set; } = null!;
    public string AuthorId { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime Date { get; set; }

    public List<SubCommentDTO> SubComments { get; set; } = new();
}