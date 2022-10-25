using Domain;

namespace MotoGuild_API.Repository.Interface;

public interface IGroupParticipantsRepository : IDisposable
{
    IEnumerable<User> GetAll(int groupId);
    User Get(int groupId, int participantId);

    User GetUser(int userId);
    User GetUserByName(string name);
    void AddParticipantByUserId(int groupId, int userId);
    void AddParticipantByUserName(int groupId, string name);
    void DeleteParticipantByUserId(int groupId, int userId);
    void Update(User user);
    bool GroupExist(int groupId);
    bool UserExits(string userName);
    bool UserInGroup(int groupId, string userName);
    string GetUserName(int userId);
    void Save();
}