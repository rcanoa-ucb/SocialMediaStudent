using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
namespace SocialMedia.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly SocialMediaContext _context;
    public UnitOfWork(SocialMediaContext context)
    {
        _context = context;
        Posts = new BaseRepository<Post>(context);
        Users = new BaseRepository<User>(context);
        Comments = new BaseRepository<Comment>(context);
    }
    public IBaseRepository<Post> Posts { get; }
    public IBaseRepository<User> Users { get; }
    public IBaseRepository<Comment> Comments { get; }
    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    public void Dispose() => _context.Dispose();
}
