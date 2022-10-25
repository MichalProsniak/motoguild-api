using Domain;

namespace MotoGuild_API.Repository.Interface;

public interface ICommentRepository : IDisposable
{
    IEnumerable<Comment>? GetAll(int postId);
    IEnumerable<Comment>? GetAll();
    Comment? Get(int commentId);
    void Insert(Comment comment, int postId, string userName);
    void Delete(int commentId);
    void Update(Comment comment);
    void Save();
}