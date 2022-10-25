using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Repository;

public class UserRepository : IUserRepository
{
    private readonly MotoGuildDbContext _context;

    private bool disposed;

    public UserRepository(MotoGuildDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users
            .ToList();
    }

    public User Get(int userId)
    {
        return _context.Users
            .Include(u => u.Groups).ThenInclude(g => g.Owner)
            .Include(u => u.Groups).ThenInclude(g => g.Participants)
            .Include(u => u.OwnedGroups).ThenInclude(g => g.Owner)
            .Include(u => u.PendingGroups).ThenInclude(g => g.Owner)
            .Include(u => u.Events).ThenInclude(e => e.Owner)
            .Include(u => u.OwnedEvents).ThenInclude(e => e.Owner)
            .Include(u => u.Rides).ThenInclude(r => r.Owner)
            .Include(u => u.OwnedRides).ThenInclude(r => r.Owner)
            .Include(u => u.Routes).ThenInclude(r => r.Stops)
            .FirstOrDefault(u => u.Id == userId);
    }

    public bool UserNameExist(string name)
    {
        return _context.Users.FirstOrDefault(u => u.UserName == name) != null;
    }
    public bool UserEmailExist(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email) != null;
    }

    public User GetUserByName(string name)
    {
        return _context.Users.FirstOrDefault(u => u.UserName == name);
    }

    public User FindUserByRefreshToken(string token)
    {
        return _context.Users.FirstOrDefault(u => u.RefreshToken == token);
    }

    public void Insert(User user)
    {
        _context.Users.Add(user);
    }

    public void Delete(int userId)
    {
        var user = _context.Users.Find(userId);
        _context.Users.Remove(user);
    }

    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
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