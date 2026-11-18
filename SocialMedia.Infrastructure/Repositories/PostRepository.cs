
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Queries;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly SocialMediaContext _socialMediaContext;
        private readonly IDapperContext _dapper;

        public PostRepository(
            SocialMediaContext context,
            IDapperContext dapper) : base(context)
        {
            _socialMediaContext = context;
            _dapper = dapper;
        }

        public async Task<IEnumerable<Post>> GetAllPostsByUserAsync(int userId)
        {
            return await _socialMediaContext.Posts
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsDapperAsync(int limit = 10)
        {
            return await _dapper.QueryAsync<Post>(
                Primero.unoMySql,
                new { Limit = limit }
            );
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _socialMediaContext.Posts
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdatePost(Post post)
        {
            _socialMediaContext.Posts.Update(post);
            await _socialMediaContext.SaveChangesAsync();
        }

        public async Task DeletePost(Post post)
        {
            _socialMediaContext.Posts.Remove(post);
            await _socialMediaContext.SaveChangesAsync();
        }
    }
}
