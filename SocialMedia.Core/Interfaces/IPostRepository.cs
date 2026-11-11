using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllPostsByUserAsync(int userId);

        Task<IEnumerable<Post>> GetAllPostsDapperAsync(int limit = 10);

        //Task<IEnumerable<Post>> GetAllPostsAsync();
        //Task<Post> GetPostByIdAsync(int id);
        //Task InsertPost(Post post);
        //Task UpdatePost(Post post);
        //Task DeletePost(Post post);
    }
}
