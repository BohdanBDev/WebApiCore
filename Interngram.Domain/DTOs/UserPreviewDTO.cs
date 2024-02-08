using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.DTOs
{
    public class UserPreviewDTO
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public bool Following { get; set; } = false;
    }
}
