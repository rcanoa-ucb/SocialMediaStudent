using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Services.Interfaces
{
    public interface IPostService
    {
        // 5 metodos por entitys
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        Task InsertPost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(Post post);
    }
}