using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Ride
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public string Description { get; set; }
        public ICollection<User> Participants { get; set; }
        public Route? Route { get; set; }
        public DateTime StartTime { get; set; }
        public int Estimation { get; set; }
        public ICollection<Post> Posts { get; set; }
        public int MinimumRating { get; set; }

    }
}
