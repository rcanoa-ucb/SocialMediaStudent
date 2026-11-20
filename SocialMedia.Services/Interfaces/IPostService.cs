using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync(PostQueryFilters? filters);
        Task<IEnumerable<Post>> GetAllPostsDapperAsync(int limit = 10);
        Task<Post> GetPostsByIdAsync(int id);
        Task InsertPost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(int id);
    }
}
