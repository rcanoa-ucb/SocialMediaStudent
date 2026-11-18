using SocialMedia.Core.Entities;

namespace SocialMedia.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllCommentsDapperAsync(int limit = 10);
    }
}
