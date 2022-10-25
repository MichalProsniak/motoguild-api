using Domain;
using MotoGuild_API.Helpers;

namespace MotoGuild_API.Repository.Interface;

public interface IRideRepository : IDisposable
{
    IEnumerable<Ride> GetAll(PaginationParams @params);
    Ride Get(int rideId);
    int TotalNumberOfRides();
    void Insert(Ride ride, string userName);
    void Delete(int rideId);
    void Update(Ride ride);
    void Save();
}