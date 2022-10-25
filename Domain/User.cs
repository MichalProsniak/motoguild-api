namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public int? PhoneNumber { get; set; }
        public double Rating { get; set; }
        public string? Image {get; set;}
        public ICollection<Group> Groups { get; set; } 
        public ICollection<Group> OwnedGroups { get; set; } 
        public ICollection<Group> PendingGroups { get; set; } 
        public ICollection<Event> Events { get; set; } 
        public ICollection<Event> OwnedEvents { get; set; } 
        public ICollection<Ride> Rides { get; set; } 
        public ICollection<Ride> OwnedRides { get; set; } 
        public ICollection<Route> Routes { get; set; } 

    }
}
