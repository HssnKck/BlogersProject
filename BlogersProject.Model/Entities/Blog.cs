﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogersProject.Model.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogFirst { get; set; }
        public string BlogPost { get; set; }
        public string Blogger { get; set; }
        public DateTime BlogDate { get; set; } = DateTime.Now;
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

    }
}
