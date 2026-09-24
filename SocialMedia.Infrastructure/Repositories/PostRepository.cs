using SocialMedia.Core.Entities;
using SocialMedia.Core.Interafaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        // Implementar los métodos de IPostRepository
        public Task DeletePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertPost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
