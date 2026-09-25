using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository //tienen que ser asincrono, a menos qeu no sorpete sincronos
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();   // recuperar todos los post(contratos sin modifciador de accesos)
        Task<Post> GetPostByIdAsync(int id); // obtener un solo registro
        Task InsertPost(Post post); //isnertar
        Task UpdatePost(Post post);
        Task DeletePost(Post post);
    }
}
