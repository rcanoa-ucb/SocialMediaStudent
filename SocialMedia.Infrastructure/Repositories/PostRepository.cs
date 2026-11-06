using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : 
        BaseRepository<Post>, IPostRepository
    {   
        //private readonly SocialMediaContext _socialMediaContext;
        public PostRepository(SocialMediaContext context)
            : base(context)
        {
            
        }

        public async Task<IEnumerable<Post>> GetAllPostsByUserAsync(int userId)
        {
            return await _entities.Where(x => x.UserId == userId).ToListAsync();
        }
                
        //public async Task<IEnumerable<Post>> GetAllPostsAsync()
        //{
        //    var posts = await _socialMediaContext.Posts.ToListAsync();
        //    return posts;
        //}

        //public async Task<Post> GetPostByIdAsync(int id)
        //{
        //    var post = await _socialMediaContext.Posts.
        //        FirstOrDefaultAsync(x => x.Id == id);
        //    return post;
        //}

        //public async Task InsertPost(Post post)
        //{
        //    _socialMediaContext.Posts.Add(post);
        //    await _socialMediaContext.SaveChangesAsync();
        //}

        //public async Task UpdatePost(Post post)
        //{
        //    _socialMediaContext.Posts.Update(post);
        //    await _socialMediaContext.SaveChangesAsync();
        //}
        //public async Task DeletePost(Post post)
        //{
        //    _socialMediaContext.Posts.Remove(post);
        //    await _socialMediaContext.SaveChangesAsync();
        //}
    }
}
