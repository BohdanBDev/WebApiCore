using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public DateTime BirthDay { get; set; }
        public string City { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public string Avatar { get; set; } = null!;

        public List<string> Subscribers { get; set; } = null!;
        public List<string> Subscriptions { get; set; } = null!;
    }
}
