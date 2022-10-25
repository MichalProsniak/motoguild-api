using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Repository;

public class FeedRepository : IFeedRepository
{
    private readonly MotoGuildDbContext _context;

    private bool disposed;

    public FeedRepository(MotoGuildDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Feed> GetAll()
    {
        return _context.Feed
            .Include(g => g.Posts).ThenInclude(p => p.Author)
            .ToList();
    }

    public Feed Get(int id)
    {
        return _context.Feed.Find(id);
    }

    public void Insert(Feed feed)
    {
        _context.Feed.Add(feed);
    }

    public void Delete(int feedId)
    {
        var feed = _context.Feed
            .Include(g => g.Posts)
            .FirstOrDefault(g => g.Id == feedId);
        _context.Feed.Remove(feed);
    }

    public void Update(Feed group)
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