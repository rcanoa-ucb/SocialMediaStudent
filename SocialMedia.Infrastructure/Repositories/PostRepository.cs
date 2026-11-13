using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : 
        BaseRepository<Post>, IPostRepository
    {
        private readonly IDapperContext _dapper;
        //private readonly SocialMediaContext _socialMediaContext;
        public PostRepository(SocialMediaContext context, 
            IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }
        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _socialMediaContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return post;
        }

        public async Task<IEnumerable<Post>> 
            GetAllPostsDapperAsync(int limit = 10)
        {
            try
            {
                return await _dapper.QueryAsync<Post>
                    (Primero.unoMySql, new { Limit = limit });
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
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
