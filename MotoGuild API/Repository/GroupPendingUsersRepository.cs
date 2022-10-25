using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Repository;

public class GroupPendingUsersRepository : IGroupPendingUsersRepository
{
    private readonly MotoGuildDbContext _context;

    private bool disposed;

    public GroupPendingUsersRepository(MotoGuildDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll(int groupId)
    {
        var group = _context.Groups
            .Include(g => g.PendingUsers)
            .FirstOrDefault(g => g.Id == groupId);
        var pendingUsers = group.PendingUsers.ToList();
        return pendingUsers;
    }

    public User Get(int groupId, int pendingUserId)
    {
        var pendingUsers = GetAll(groupId);

        return pendingUsers.FirstOrDefault(p => p.Id == pendingUserId);
    }

    public User GetUser(int userId)
    {
        return _context.Users.Find(userId);
    }

    public void AddPendingUserByUserId(int groupId, int userId)
    {
        var group = _context.Groups
            .Include(g => g.PendingUsers)
            .FirstOrDefault(g => g.Id == groupId);
        var user = _context.Users
            .FirstOrDefault(u => u.Id == userId);
        group.PendingUsers.Add(user);
    }

    public void DeletePendingUserByUserId(int groupId, int userId)
    {
        var group = _context.Groups
            .Include(g => g.PendingUsers)
            .FirstOrDefault(g => g.Id == groupId);
        var user = _context.Users
            .FirstOrDefault(u => u.Id == userId);
        group.PendingUsers.Remove(user);
    }

    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public bool GroupExist(int groupId)
    {
        var group = _context.Groups.FirstOrDefault(g => g.Id == groupId);
        return group != null;
    }

    public bool UserExits(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        return user != null;
    }
    public int GetUserId(string name)
    {
        var userId = _context.Users.FirstOrDefault(u => u.UserName == name).Id;
        return userId;
    }

    public bool UserInPendingUsers(int groupId, int userId)
    {
        var group = _context.Groups.Include(g => g.PendingUsers).FirstOrDefault(g => g.Id == groupId);
        if (group != null)
        {
            var participantsIds = group.PendingUsers.Select(p => p.Id);
            return participantsIds.Contains(userId);
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