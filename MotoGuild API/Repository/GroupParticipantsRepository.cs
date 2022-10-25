using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Repository;

public class GroupParticipantsRepository : IGroupParticipantsRepository
{
    private readonly MotoGuildDbContext _context;

    private bool disposed;

    public GroupParticipantsRepository(MotoGuildDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll(int groupId)
    {
        var group = _context.Groups
            .Include(g => g.Participants)
            .FirstOrDefault(g => g.Id == groupId);
        var participants = group.Participants.ToList();
        return participants;
    }

    public User Get(int groupId, int participantId)
    {
        var participants = GetAll(groupId);

        return participants.FirstOrDefault(p => p.Id == participantId);
    }

    public User GetUser(int userId)
    {
        return _context.Users.Find(userId);
    }
    public User GetUserByName(string name)
    {
        return _context.Users.FirstOrDefault(u=>u.UserName == name);
    }

    public void AddParticipantByUserId(int groupId, int userId)
    {
        var group = _context.Groups
            .Include(g => g.Participants)
            .FirstOrDefault(g => g.Id == groupId);
        var user = _context.Users
            .FirstOrDefault(u => u.Id == userId);
        group.Participants.Add(user);
    }
    public void AddParticipantByUserName(int groupId, string name)
    {
        var group = _context.Groups
            .Include(g => g.Participants)
            .FirstOrDefault(g => g.Id == groupId);
        var user = _context.Users
            .FirstOrDefault(u => u.UserName == name);
        group.Participants.Add(user);
    }

    public void DeleteParticipantByUserId(int groupId, int userId)
    {
        var group = _context.Groups
            .Include(g => g.Participants)
            .FirstOrDefault(g => g.Id == groupId);
        var user = _context.Users
            .FirstOrDefault(u => u.Id == userId);
        group.Participants.Remove(user);
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

    public bool UserExits(string name)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == name);
        return user != null;
    }

    public bool UserInGroup(int groupId, string userName)
    {
        var group = _context.Groups.Include(g => g.Participants).FirstOrDefault(g => g.Id == groupId);
        if (group != null)
        {
            var participantsNames = group.Participants.Select(p => p.UserName);
            return participantsNames.Contains(userName);
        }

        return false;
    }

    public string GetUserName(int id)
    {
        var userName = string.Empty;
        userName = _context.Users.FirstOrDefault(u => u.Id == id).UserName;
        return userName;
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