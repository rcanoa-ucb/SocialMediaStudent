using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public Task DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetCommentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}