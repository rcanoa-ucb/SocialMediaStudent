using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Interafaces
{
    public interface IPostRepository
    {   // para convertir a asincrono se coloca Tak es no recomendable porsi
        Task<IEnumerable<Post>> GetAllPostsAsync(); // Método para obtener todos los posts
        Task<Post> GetPostByIdAsync(int id); // Método para obtener un post por su ID
        Task InsertPost(Post post);// Método para insertar un nuevo post
        Task UpdatePost(Post post); // Método para actualizar un post existente
        Task DeletePost(Post post); //Metodo para eliminar un post

        // Definir los métodos que se van a utilizar para acceder a los datos de los posts
    }
}
