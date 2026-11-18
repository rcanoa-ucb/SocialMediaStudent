using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Services.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetAllCommentsAsync(CommentQueryFilters? filters = null);
    Task<IEnumerable<Comment>> GetAllCommentsDapperAsync(int limit = 10);
    Task<Comment?> GetCommentByIdAsync(int id);
    Task InsertComment(Comment comment);
    Task UpdateComment(Comment comment);
    Task DeleteComment(int id);
}
