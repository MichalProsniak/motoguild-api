using Domain;

namespace MotoGuild_API.Repository.Interface;

public interface IRouteStopsRepository : IDisposable
{
    IEnumerable<Stop> GetAll(int routeId);
    Stop Get(int stopId, int routeId);
    void Insert(Stop stop, int routeId);
    void Delete(int stopId, int routeId);
    void Update(Stop stop);
    bool StopExistsInRoute(int stopId, int routeId);
    void Save();
}