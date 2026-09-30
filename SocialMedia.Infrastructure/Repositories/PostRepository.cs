using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext  _socialMediaContext;

        //se puede acceder a los metodos de _socialMediaConetxt
        public PostRepository(SocialMediaContext socialMediaContext)  
        {
            _socialMediaContext = socialMediaContext;   

        }
        // task proceso asincrono debe existir una palabra await
        public async Task<IEnumerable<Post>> GetAllPostsAsync()   //recuperar todos los registros de la tablas post
        {
            var posts = await _socialMediaContext.Posts.ToListAsync(); //select * post from en este caso posts
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _socialMediaContext.Posts.FirstOrDefaultAsync(x=>x.Id==id); //la primera coincidencia que se encuentre devolveme el dato o nulo
            return post;
        }

        public async Task InsertPost(Post post)  //task<> sin eso es void 
        {
            _socialMediaContext.Posts.Add(post);     //transaccion metodos que afectan a bd
            await _socialMediaContext.SaveChangesAsync(); //commit        
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
