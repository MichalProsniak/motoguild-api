using Data;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Helpers;
using MotoGuild_API.Repository.Interface;
using Route = Domain.Route;

namespace MotoGuild_API.Repository;

public class RouteRepository : IRouteRepository
{
    private readonly MotoGuildDbContext _context;

    private bool disposed;

    public RouteRepository(MotoGuildDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Route> GetAll(PaginationParams @params)
    {
        return _context.Routes
            .Include(r => r.Owner)
            .Include(r => r.Stops)
            .Skip((@params.Page - 1) * @params.ItemsPerPage)
            .Take(@params.ItemsPerPage)
            .ToList();
    }

    public IEnumerable<Route> GetAllWithoutPagination()
    {
        return _context.Routes
            .Include(r => r.Owner)
            .Include(r => r.Stops)
            .ToList();
    }

    public int TotalNumberOfRoutes()
    {
        return _context.Routes.Count();
        ;
    }

    public IEnumerable<Route> GetFiveOrderByRating(PaginationParams @params)
    {
        return _context.Routes
            .Include(r => r.Owner)
            .Include(r => r.Stops)
            .OrderByDescending(r => r.Rating)
            .Skip((@params.Page - 1) * @params.ItemsPerPage)
            .Take(@params.ItemsPerPage)
            .ToList();
    }

    public Route Get(int id)
    {
        return _context.Routes
            .Include(r => r.Owner)
            .Include(r => r.Posts).ThenInclude(p => p.Author)
            .Include(r => r.Stops)
            .FirstOrDefault(r => r.Id == id);
    }

    public void Insert(Route route, string userName)
    {
        var ownerFull = _context.Users.FirstOrDefault(u => u.UserName == userName);
        route.Owner = ownerFull;
        _context.Routes.Add(route);
    }

    public void Delete(int id)
    {
        var ridesWithRoute = _context.Rides
            .Include(r => r.Route)
            .Where(r => r.Route.Id == id);
        foreach (var ride in ridesWithRoute) ride.Route = null;
        var route = _context.Routes
            .Include(g => g.Posts)
            .Include(r => r.Posts)
            .FirstOrDefault(g => g.Id == id);
        _context.Routes.Remove(route);
    }

    public void Update(Route route)
    {
        _context.Entry(route).State = EntityState.Modified;
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