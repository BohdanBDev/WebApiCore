using Interngram.Repository.Core.Models;

namespace Interngram.Repository.Models;

public class Comment
{
    public string Id { get; set; } = null!;
    public string PostId { get; set; } = null!;
    public string AuthorId { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime Date { get; set; }

    public List<SubComment>? SubComments { get; set; }
}