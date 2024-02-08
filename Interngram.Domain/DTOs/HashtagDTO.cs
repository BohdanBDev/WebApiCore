﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.DTOs
{
    public class HashtagDTO
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public DateTime Date { get; set; }
        public int PostsCount { get; set; }

        public List<string>? PostsId { get; set; }
    }
}
