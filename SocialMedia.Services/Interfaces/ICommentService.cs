using System;
using System.Collections.Generic;
using System.Text;

using SocialMedia.Core.Entities;

namespace SocialMedia.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllCommentsDapperAsync(int limit = 10);
        Task<IEnumerable<Comment>> GetAllComments();
        Task<Comment> GetComment(int id);
        Task InsertComment(Comment comment);
        Task<bool> UpdateComment(Comment comment);
        Task<bool> DeleteComment(int id);
    }
}