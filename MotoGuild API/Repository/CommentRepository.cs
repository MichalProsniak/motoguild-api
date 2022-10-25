using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly MotoGuildDbContext _context;

    private bool disposed;

    public CommentRepository(MotoGuildDbContext context)
    {
        _context = context;
    }

    public void Delete(int commentId)
    {
        var comment = _context.Comments.FirstOrDefault(c => c.Id == commentId);
        if (comment != null) _context.Comments.Remove(comment);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Comment? Get(int commentId)
    {
        var comment = _context.Comments.Include(c => c.Author).FirstOrDefault(c => c.Id == commentId);
        if (comment != null) return comment;
        return null;
    }

    public IEnumerable<Comment>? GetAll(int postId)
    {
        var post = _context.Posts.Include(c => c.Comments).ThenInclude(c => c.Author)
            .FirstOrDefault(c => c.Id == postId);

        if (post == null) return null;

        return post.Comments;
    }

    public IEnumerable<Comment>? GetAll()
    {
        return _context.Comments.Include(c => c.Author).ToList();
    }

    public void Insert(Comment comment, int postId, string userName)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
        comment.Author = user;
        _context.Posts.Include(p => p.Comments).FirstOrDefault(p => p.Id == postId).Comments.Add(comment);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Update(Comment comment)
    {
        _context.Entry(comment).State = EntityState.Modified;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
            if (disposing)
                _context.Dispose();
        disposed = true;
    }
}