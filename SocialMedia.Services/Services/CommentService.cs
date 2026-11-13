using System;
using System.Collections.Generic;
using System.Text;

using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsDapperAsync(int limit = 10)
        {
            return await _unitOfWork.CommentRepository.GetAllCommentsDapperAsync(limit);
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _unitOfWork.CommentRepository.GetAll();
        }

        public async Task<Comment> GetComment(int id)
        {
            return await _unitOfWork.CommentRepository.GetById(id);
        }

        public async Task InsertComment(Comment comment)
        {
            await _unitOfWork.CommentRepository.Add(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateComment(Comment comment)
        {
            _unitOfWork.CommentRepository.Update(comment);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteComment(int id)
        {
            await _unitOfWork.CommentRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
