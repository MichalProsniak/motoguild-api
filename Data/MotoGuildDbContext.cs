using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MotoGuildDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feed> Feed { get; set; }

        public MotoGuildDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
        
            modelBuilder.Entity<User>()
                .HasMany(a => a.OwnedEvents);
            modelBuilder.Entity<User>()
                .HasMany(a => a.OwnedGroups);
            modelBuilder.Entity<User>()
                .HasMany(a => a.OwnedRides);


            modelBuilder.Entity<Group>()
                .HasOne(a => a.Owner);
            modelBuilder.Entity<Ride>()
                .HasOne(a => a.Owner);
            modelBuilder.Entity<Route>()
                .HasOne(a => a.Owner);
            modelBuilder.Entity<Event>()
                .HasOne(a => a.Owner);


            modelBuilder.Entity<Event>()
                .HasMany(a => a.Participants)
                .WithMany(b => b.Events);
            modelBuilder.Entity<Ride>()
                .HasMany(a => a.Participants)
                .WithMany(b => b.Rides);
            modelBuilder.Entity<Group>()
                .HasMany(a => a.Participants)
                .WithMany(b => b.Groups);
            modelBuilder.Entity<Group>()
                .HasMany(a => a.PendingUsers)
                .WithMany(b => b.PendingGroups);



        }
    
}
}
