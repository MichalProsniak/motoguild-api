using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Helpers;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Repository;

public class GroupRepository : IGroupRepository
{
    private readonly MotoGuildDbContext _context;

    private bool disposed;

    public GroupRepository(MotoGuildDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Group> GetAll(PaginationParams @params)
    {
        return _context.Groups
            .Include(g => g.Owner)
            .Include(g => g.Participants)
            .Include(g => g.PendingUsers)
            .Include(g => g.Posts).ThenInclude(p => p.Author)
            .OrderByDescending(g => g.Participants.Count)
            .Skip((@params.Page - 1) * @params.ItemsPerPage)
            .Take(@params.ItemsPerPage)
            .ToList();
    }

    public int TotalNumberOfGroups()
    {
        return _context.Groups.Count();
    }

    public Group Get(int id)
    {
        return _context.Groups.Include(g => g.Owner)
            .Include(g => g.Participants)
            .Include(g => g.PendingUsers)
            .Include(g => g.Posts).ThenInclude(p => p.Author)
            .FirstOrDefault(g => g.Id == id);
    }

    public void Insert(Group group, string userName)
    {
        var ownerFull = _context.Users.FirstOrDefault(u => u.UserName == userName);
        group.Owner = ownerFull;
        _context.Groups.Add(group);
        group.Participants.Add(group.Owner);
    }

    public void Delete(int groupId)
    {
        var group = _context.Groups
            .Include(g => g.Posts)
            .FirstOrDefault(g => g.Id == groupId);
        _context.Groups.Remove(group);
    }

    public void Update(Group group)
    {
        _context.Entry(group).State = EntityState.Modified;
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