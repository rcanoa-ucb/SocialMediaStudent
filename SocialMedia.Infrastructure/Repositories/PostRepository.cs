using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;


namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository //INYECCION LLAMAR AL PADRE
    {
        private readonly SocialMediaContext _socialMediaContext;        //SE LO INYECTA EN LA ENTRADA DE ALGO, POR EL METODO DEL CONSTRUCTOR //declarar la variable
                                                               
        public PostRepository(SocialMediaContext socialMediaContext)    //DENTRO DEL CONSTRUCTOR , LE ASIGNAS EL DATO DEL CONSTRUCTOR AL DE ARRIBA //poner al constructor, inyeccion al parametro
        {
            _socialMediaContext = socialMediaContext;                   //asignar al parametro del constructor
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()   //OBTENER TODOS LOS POSTS
        {
            var posts = await _socialMediaContext.Posts.ToListAsync();  //Es para modificar tus tablas de asincronico //SELECT * FROM Posts
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(int id)   //REGISTRO DE POSTS DE TODOS LOS IDS
        { 
            var post = await _socialMediaContext.Posts.   
                FirstOrDefaultAsync(x => x.Id == id); //EL PRIMER VALOR QUE SE MUESTRA CON UNA CONDICIONAL x = variable temporal. Expresion lamda, busvcar el primer elemento
            return post;
        }

        public async Task InsertPost(Post post)  //ES VOID QUE SI NO TIENE NADA Task<Post> solo Task,
                                                 //POR EJEMPLO SIN EL ASYNC, siempre cuando escribas await hay que colocar el async
        {
            _socialMediaContext.Posts.Add(post);          //Es una transaccion para insertar, es todo lo que afecta a la base de datos o la estructura.
            await _socialMediaContext.SaveChangesAsync(); //es como cargar una bala y disparar
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
