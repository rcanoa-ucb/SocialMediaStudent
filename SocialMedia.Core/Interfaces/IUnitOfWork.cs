using SocialMedia.Core.Entities;
namespace SocialMedia.Core.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Post> Posts { get; }
    IBaseRepository<User> Users { get; }
    IBaseRepository<Comment> Comments { get; }
    Task<int> SaveChangesAsync();
}
