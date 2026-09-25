using SocialMedia.Core.Entities;
using SocialMedia.Core.Interafaces;
using SocialMedia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        // Implementar los métodos de IPostRepository

        //Se hara una injeccion de dependencias en base al contexto de la base de datos, para poder acceder a los datos de la base de datos.
        private readonly SocialMediaContext _socialMediaContext;
        public PostRepository(SocialMediaContext socialMediaContext)
        {
            this._socialMediaContext = socialMediaContext;
        }
        public async Task<IEnumerable<Post>> GetAllPostsAsync()// Cada que usamos tasks eso significa que es un proceso asincrono eso significa que tu vas a esperar el resultado y cuando se utiliza esto se utiliza await
        {
            var posts = await _socialMediaContext.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _socialMediaContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return post;
        }

        public async Task InsertPost(Post post)
        {
            _socialMediaContext.Posts.Add(post); //si lo dejamos asi falta el await y el savechangesasync para que se guarde en la base de datos
            await _socialMediaContext.SaveChangesAsync();//se usa await para que se espere a que se guarde en la base de datos y no se continue con el flujo del programa hasta que se guarde en la base de datos
        }

        public Task UpdatePost(Post post)
        {
           _socialMediaContext.Posts.Update(post);
            return _socialMediaContext.SaveChangesAsync();
        }
        public Task DeletePost(Post post)
        {
            _socialMediaContext.Posts.Remove(post);
            return _socialMediaContext.SaveChangesAsync();
        }
    }
}
