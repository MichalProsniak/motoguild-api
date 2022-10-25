using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Repository;

public class RideParticipantsRepository : IRideParticipantsRepository
{
    private readonly MotoGuildDbContext _context;

    private bool disposed;

    public RideParticipantsRepository(MotoGuildDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll(int rideId)
    {
        var ride = _context.Rides
            .Include(g => g.Participants)
            .FirstOrDefault(g => g.Id == rideId);
        var participants = ride.Participants.ToList();
        return participants;
    }

    public User Get(int rideId, int participantId)
    {
        var participants = GetAll(rideId);

        return participants.FirstOrDefault(p => p.Id == participantId);
    }

    public User GetUser(int userId)
    {
        return _context.Users.Find(userId);
    }

    public User GetUserByName(string userName)
    {
        return _context.Users.FirstOrDefault(u => u.UserName == userName);
    }

    public void AddParticipantByUserId(int rideId, int userId)
    {
        var ride = _context.Rides
            .Include(g => g.Participants)
            .FirstOrDefault(g => g.Id == rideId);
        var user = _context.Users
            .FirstOrDefault(u => u.Id == userId);
        ride.Participants.Add(user);
    }

    public void AddParticipantByUserName(int rideId, string userName)
    {
        var ride = _context.Rides
            .Include(g => g.Participants)
            .FirstOrDefault(g => g.Id == rideId);
        var user = _context.Users
            .FirstOrDefault(u => u.UserName == userName);
        ride.Participants.Add(user);
    }

    public void DeleteParticipantByUserId(int rideId, int userId)
    {
        var ride = _context.Rides
            .Include(g => g.Participants)
            .FirstOrDefault(g => g.Id == rideId);
        var user = _context.Users
            .FirstOrDefault(u => u.Id == userId);
        ride.Participants.Remove(user);
    }

    public void DeleteParticipantByUserName(int rideId, string userName)
    {
        var ride = _context.Rides
            .Include(g => g.Participants)
            .FirstOrDefault(g => g.Id == rideId);
        var user = _context.Users
            .FirstOrDefault(u => u.UserName == userName);
        ride.Participants.Remove(user);
    }

    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public bool RideExist(int rideId)
    {
        var ride = _context.Rides.FirstOrDefault(g => g.Id == rideId);
        return ride != null;
    }

    public bool UserExits(int userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        return user != null;
    }

    public bool UserExits(string userName)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
        return user != null;
    }

    public bool UserInRide(int rideId, int userId)
    {
        var ride = _context.Rides.Include(g => g.Participants).FirstOrDefault(g => g.Id == rideId);
        if (ride != null)
        {
            var participantsIds = ride.Participants.Select(p => p.Id);
            return participantsIds.Contains(userId);
        }

        return false;
    }

    public bool UserInRide(int rideId, string userName)
    {
        var ride = _context.Rides.Include(g => g.Participants).FirstOrDefault(g => g.Id == rideId);
        if (ride != null)
        {
            var participantsIds = ride.Participants.Select(p => p.UserName);
            return participantsIds.Contains(userName);
        }

        return false;
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
            if (disposing)
                _context.Dispose();
        disposed = true;
    }
}