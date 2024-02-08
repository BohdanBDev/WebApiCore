using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.DTOs
{
    public class SubCommentDTO
    {
        public string Id { get; set; } = null!;
        public string ParentId { get; set; } = null!;
        public string PostId { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
