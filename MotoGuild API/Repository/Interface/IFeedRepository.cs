using Domain;

namespace MotoGuild_API.Repository.Interface;

public interface IFeedRepository : IDisposable
{
    IEnumerable<Feed> GetAll();
    Feed Get(int feedId);
    void Insert(Feed feed);
    void Delete(int feedId);
    void Update(Feed feed);
    void Save();
}