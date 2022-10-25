using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Helpers;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Repository;

public class EventRepository : IEventRepository
{
    private readonly MotoGuildDbContext _context;

    private bool disposed;

    public EventRepository(MotoGuildDbContext context)
    {
        _context = context;
    }

    public int TotalNumberOfEvents()
    {
        return _context.Events.Count();
    }
    public IEnumerable<Event> GetAll(PaginationParams @params)
    {
        return _context.Events
            .Include(g => g.Owner)
            //.Include(g => g.Participants)
            //.Include(g => g.Posts)
            .Skip((@params.Page - 1) * @params.ItemsPerPage)
            .Take(@params.ItemsPerPage)
            .ToList();
    }

    public Event Get(int id)
    {
        return _context.Events.Find(id);
    }

    public void Insert(Event eve)
    {
        var ownerFull = _context.Users.FirstOrDefault(u => u.Id == eve.Owner.Id);
        eve.Owner = ownerFull;
        eve.Participants.Add(ownerFull);
        _context.Events.Add(eve);
    }

    public void Delete(int eveId)
    {
        var eve = _context.Events
            .Include(g => g.Posts)
            .FirstOrDefault(g => g.Id == eveId);
        _context.Events.Remove(eve);
    }

    public void Update(Event eve)
    {
        _context.Entry(eve).State = EntityState.Modified;
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