using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public string Description { get; set; }
        public ICollection<User> Participants { get; set; } = new List<User>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public bool IsPrivate { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? GroupImage { get; set; }
        public ICollection<User> PendingUsers { get; set; } = new List<User>();
        public double Rating { get; set; }
    }
}
