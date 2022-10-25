using MotoGuild_API.Helpers;
using Route = Domain.Route;

namespace MotoGuild_API.Repository.Interface;

public interface IRouteRepository : IDisposable
{
    IEnumerable<Route> GetAll(PaginationParams @params);
    IEnumerable<Route> GetAllWithoutPagination();
    IEnumerable<Route> GetFiveOrderByRating(PaginationParams @params);
    int TotalNumberOfRoutes();
    Route Get(int routeId);
    void Insert(Route route, string userName);
    void Delete(int routeId);
    void Update(Route route);
    void Save();
}