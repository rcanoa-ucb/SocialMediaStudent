using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetComments();
        Task<Comment> GetComment(int id);
        Task InsertComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task<bool> DeleteComment(int id);
    }
}