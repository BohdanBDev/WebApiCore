using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.Filters
{
    public class GetUserFilter
    {
        public string? Phone { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public string? Id { get; set; }
    }
}
