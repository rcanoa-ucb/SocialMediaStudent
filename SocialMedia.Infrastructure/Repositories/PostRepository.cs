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
        private readonly SocialMediaContext _socialMediaContext; //creamos la variable solo para lectura, todo con readonly
         public PostRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext; //hasta aqui los tres pasos realizados son iyeccion por dependencia
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _socialMediaContext.Posts.ToListAsync(); //variables deben ser significativas, equivalente a en el SQL al SELECT * FROM POST
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(int id) //equivalente al SELECT * FROM POST WHER ID = ID
        {
            var post = await _socialMediaContext.Posts.FirstOrDefaultAsync(x => x.Id == id); //FirstOrDefaultAsync( X -> Equivale a una variable temporal, x => x.Id == id La condicion) el primer valor
            return post;
        }

        public async Task InsertPost(Post post) //TASK sin <> equivale a un void, no devuelve nada, async /*Esto lo agrgamos nosotros*/
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
