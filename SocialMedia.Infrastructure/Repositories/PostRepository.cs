using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _socialMediaContext;

        public PostRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _socialMediaContext.Posts.AsNoTracking().ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _socialMediaContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertPost(Post post)
        {
            _socialMediaContext.Posts.Add(post);
            await _socialMediaContext.SaveChangesAsync();
        }

        public async Task UpdatePost(Post post)
        {
            _socialMediaContext.Posts.Update(post);
            await _socialMediaContext.SaveChangesAsync();
        }

        public async Task DeletePost(Post post)
        {
            // La FK no tiene cascada: se borran primero los comentarios del post.
            // Todo se guarda en un solo SaveChanges (una sola transacción).
            var comments = _socialMediaContext.Comments.Where(c => c.PostId == post.Id);
            _socialMediaContext.Comments.RemoveRange(comments);
            _socialMediaContext.Posts.Remove(post);
            await _socialMediaContext.SaveChangesAsync();
        }
    }
}
