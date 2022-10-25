using Domain;

namespace MotoGuild_API.Repository.Interface;

public interface IRideParticipantsRepository : IDisposable
{
    IEnumerable<User> GetAll(int rideId);
    User Get(int rideId, int participantId);

    User GetUser(int userId);
    User GetUserByName(string userName);
    void AddParticipantByUserId(int rideId, int userId);
    void AddParticipantByUserName(int rideId, string userName);
    void DeleteParticipantByUserId(int rideId, int userId);
    void DeleteParticipantByUserName(int rideId, string userName);
    void Update(User user);
    bool RideExist(int rideId);
    bool UserExits(int userId);
    bool UserExits(string userName);
    bool UserInRide(int rideId, int userId);
    bool UserInRide(int rideId, string userName);
    void Save();
}