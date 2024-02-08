using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.DTOs
{
    public class SubCommentCreateDTO : CommentCreateDTO
    {
        public string ParentId { get; set; } = null!;
    }
}
