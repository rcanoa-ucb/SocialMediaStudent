using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories //la clase que se conecta con los datos
{
    public class PostRepository : IPostRepository
    { //Nomenclatura directrices //inyercion de dependencias
        private readonly SocialMediaContext _socialMediaContext;
        public PostRepository(SocialMediaContext socialMediaContext) //que tipo de dato quiero inyectar
        {
            _socialMediaContext  = socialMediaContext;
        }
        
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _socialMediaContext.Posts.ToListAsync();
            return posts; 
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            //var acepta cualquier tipo de dato 
            var posts = await _socialMediaContext.Posts. //await que metodo va llamar
                FirstOrDefaultAsync(x => x.Id == id); //expresion Landa
            return posts;
        }

        public async Task InsertPost(Post post) //cuando no es <> el metodo no devuelve nada(void)
        { //transaccion cada modificacion que se hace en el registro 
            _socialMediaContext.Posts.Add(post); //insertar una bala a la pistola pero sin disparar
            await _socialMediaContext.SaveChangesAsync();//await dispara
        }

        public async Task UpdatePost(Post post) //async asincrono
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
