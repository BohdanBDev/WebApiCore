using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.DTOs
{
    public class PostCreateDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string AuthorId { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
