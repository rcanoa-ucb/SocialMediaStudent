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
        // INI Inyeccion dependencia
        private readonly SocialMediaContext _socialMediaContext; //Parece que esto permite la conexion la BD

        public PostRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
        }
        // FIN Inyeccion dependencia

        public async Task<IEnumerable<Post>> GetAllPostsAsync() // Async: Proceso Asyncrono, en otro lado se espera el resultado (siempre usar "await")
        {
            var posts = await _socialMediaContext.Posts.ToListAsync(); // Muestra todo (SELECT * FROM)
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _socialMediaContext.Posts.FirstOrDefaultAsync(x => x.Id == id); // Muestra 1 (SELECT * FROM WHERE id =)
            return post;
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
            _socialMediaContext.Posts.Remove(post);
            await _socialMediaContext.SaveChangesAsync();
        }
    }
}