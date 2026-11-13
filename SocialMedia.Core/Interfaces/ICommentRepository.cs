using SocialMedia.Core.Entities;
using System.Collections.Generic;

namespace SocialMedia.Core.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllCommentsDapperAsync(int limit = 10);
    }
}