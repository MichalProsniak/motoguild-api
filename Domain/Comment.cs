using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public DateTime CreateTime { get; set; }
        public string Content { get; set; }
    }
}
