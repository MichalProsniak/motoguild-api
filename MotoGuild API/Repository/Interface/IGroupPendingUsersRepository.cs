using Domain;

namespace MotoGuild_API.Repository.Interface;

public interface IGroupPendingUsersRepository : IDisposable
{
    IEnumerable<User> GetAll(int groupId);
    User Get(int groupId, int participantId);

    User GetUser(int userId);
    void AddPendingUserByUserId(int groupId, int userId);
    void DeletePendingUserByUserId(int groupId, int userId);
    void Update(User user);
    bool GroupExist(int groupId);
    int GetUserId(string userName);
    bool UserExits(int userId);
    bool UserInPendingUsers(int groupId, int userId);
    void Save();
}