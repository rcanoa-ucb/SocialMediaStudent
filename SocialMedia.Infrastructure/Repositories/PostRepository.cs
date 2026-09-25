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
        // una inyeccion de dependencia del contexto en el repo
        private readonly SocialMediaContext _socialMediaContext;

        public PostRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext; //inyeccion de dependencia
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {// cada Task necesita un await
            var posts = await _socialMediaContext.Posts.ToListAsync(); // SELECTT * FROM Post
            return posts;
        }// context = base de datos

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _socialMediaContext.Posts.FirstOrDefaultAsync(x => x.Id == id);// <- expresion lambda
            // search all list, till first same id and send that back;
            return post;
            throw new NotImplementedException();
        }

        public  async Task InsertPost(Post post) // task without <> means void
        {
            _socialMediaContext.Posts.Add(post);// insertar una bala
            await _socialMediaContext.SaveChangesAsync();// disparas
            // await _socialMediaContext.Posts.AddAsync(post) automatically saves (no need second line), BUT no chance for rollback
        }

        public async Task DeletePost(Post post)
        {
            _socialMediaContext.Posts.Remove(post);
            await _socialMediaContext.SaveChangesAsync();
        }

        public async Task UpdatePost(Post post)
        {
            _socialMediaContext.Posts.Update(post);
            await _socialMediaContext.SaveChangesAsync();
        }
    }
}
