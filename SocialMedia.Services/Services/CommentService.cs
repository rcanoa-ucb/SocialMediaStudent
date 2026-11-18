using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Services.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsAsync(CommentQueryFilters? filters = null)
    {
        var comments = await _unitOfWork.CommentRepository.GetAll();

        if (filters?.UserId is int userId)
            comments = comments.Where(comment => comment.UserId == userId);

        if (filters?.PostId is int postId)
            comments = comments.Where(comment => comment.PostId == postId);

        if (filters?.Date is DateOnly date)
            comments = comments.Where(comment => DateOnly.FromDateTime(comment.Date) == date);

        if (filters?.Description is { } description && !string.IsNullOrWhiteSpace(description))
            comments = comments.Where(comment =>
                comment.Description.Contains(description, StringComparison.OrdinalIgnoreCase));

        return comments;
    }

    public Task<IEnumerable<Comment>> GetAllCommentsDapperAsync(int limit = 10) =>
        _unitOfWork.CommentRepository.GetAllCommentsDapperAsync(limit);

    public async Task<Comment?> GetCommentByIdAsync(int id) =>
        await _unitOfWork.CommentRepository.GetById(id);

    public async Task InsertComment(Comment comment)
    {
        await _unitOfWork.CommentRepository.Add(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateComment(Comment comment)
    {
        await _unitOfWork.CommentRepository.Update(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteComment(int id)
    {
        await _unitOfWork.CommentRepository.Delete(id);
        await _unitOfWork.SaveChangesAsync();
    }
}
