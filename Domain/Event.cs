using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public string Description { get; set; }
        public ICollection<User> Participants { get; set; } = new List<User>();
        public string Place { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
