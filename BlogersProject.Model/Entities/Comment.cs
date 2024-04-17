using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogersProject.Model.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Commentators { get; set; }
        public string Comments { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public int? BlogId { get; set; }
        public virtual Blog? Blog { get; set; }
    }
}
