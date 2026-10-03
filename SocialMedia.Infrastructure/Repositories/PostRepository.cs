using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository  // INYECCION DE DEPOENCIDAS EN EL CONTROLADOR O CONTRATO
    {
        private readonly SocialMediaContext _socialMediaContext;//empieza en minuscula  inyectar con _
        public PostRepository(SocialMediaContext socialMediaContext)  // QUITAR _
        {
            _socialMediaContext = socialMediaContext;
        }
        public async Task<IEnumerable<Post>> GetAllPostsAsync() //task es un proceso asincrono 
        {
            // variable significable  // llamara a la base de datos y a la tabla
            var posts = await _socialMediaContext.Posts.ToListAsync(); //var acepta cuakqueir tipo de dato, 
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(int id)

        {                                             // primer valor que se muetrsar en la condicion 
            var post = await _socialMediaContext.Posts.FirstOrDefaultAsync
                (x => x.Id == id); //variable temproal (x) expresion lambda    
            return post;
        }

        public async Task InsertPost(Post post) //task sin <> es void no devuelve nada 
        {
            _socialMediaContext.Posts.Add(post); //trabsaccion es todo proceseodimeinto que afecta a la base da toas o cambia la estructura
            await _socialMediaContext.SaveChangesAsync(); //SaveChangesAsync sinonimo de commit
        }   // await esperar a que termine una operación asíncrona antes de continuar con la siguiente línea

        public async Task UpdatePost(Post post) //siempre async antes del metodo
        {
            _socialMediaContext.Posts.Update(post);
            await _socialMediaContext.SaveChangesAsync();
        }
        public async Task DeletePost(Post post) // Se agregó 'async' aquí para corregir los errores
        {
            _socialMediaContext.Posts.Remove(post);
            await _socialMediaContext.SaveChangesAsync();
        }
    }
}