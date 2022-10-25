using Domain;
using MotoGuild_API.Helpers;

namespace MotoGuild_API.Repository.Interface;

public interface IGroupRepository : IDisposable
{
    IEnumerable<Group> GetAll(PaginationParams @params);
    int TotalNumberOfGroups();
    Group Get(int groupId);
    void Insert(Group group, string userName);
    void Delete(int groupId);
    void Update(Group group);
    void Save();
}