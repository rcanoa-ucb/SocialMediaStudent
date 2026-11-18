using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly IDapperContext? _dapper;

        public PostRepository(SocialMediaContext context) : base(context)
        {
        }

        public PostRepository(SocialMediaContext context, IDapperContext dapper) : base(context)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertPost(Post post)
        {
            _entities.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(Post post)
        {
            _entities.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePost(Post post)
        {
            _entities.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}
