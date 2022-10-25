using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Owner { get; set; }
        public ICollection<Stop> Stops { get; set; }  = new List<Stop>();
        public string StartPlace { get; set; }
        public string EndingPlace { get; set; }
        public ICollection<Post> Posts { get; set; }  = new List<Post>();
        public int Rating { get; set; }
    }
}
