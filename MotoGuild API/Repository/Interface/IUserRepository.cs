using Domain;

namespace MotoGuild_API.Repository.Interface;

public interface IUserRepository : IDisposable
{
    IEnumerable<User> GetAll();
    User Get(int userId);
    bool UserNameExist(string name);
    bool UserEmailExist(string email);

    User GetUserByName(string name);

    User FindUserByRefreshToken(string token);
    void Insert(User user);
    void Delete(int userId);
    void Update(User user);
    void Save();
}