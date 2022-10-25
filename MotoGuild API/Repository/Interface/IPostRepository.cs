using Domain;

namespace MotoGuild_API.Repository.Interface;

public interface IPostRepository : IDisposable
{
    IEnumerable<Post>? GetAll();
    IEnumerable<Post>? GetAllFeed(int feedId);
    IEnumerable<Post>? GetAllGroup(int groupId);
    IEnumerable<Post>? GetAllRide(int rideId);
    IEnumerable<Post>? GetAllRoute(int routeId);
    IEnumerable<Post>? OrderedPost(IEnumerable<Post> posts);
    Post? Get(int postId);
    void InsertToFeed(Post post, int feedId, string userName);
    void InsertToGroup(Post post, int groupId, string userName);
    void InsertToRide(Post post, int rideId, string userName);
    void InsertToRoute(Post post, int routeId, string userName);
    void Delete(int postId);
    void Update(Post post);
    void Save();
}