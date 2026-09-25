using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync(); //obtiene una lista de post

        Task<Post> GetPostByIdAsync(int id); //solo obtien uno

        Task InsertPost(Post post); //inserta un post

        Task UpdatePost(Post post); //actualiza el post

        Task DeletePost(Post post); //elimina el post
    }
}
