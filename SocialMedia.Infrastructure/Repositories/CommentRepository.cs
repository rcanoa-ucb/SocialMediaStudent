using SocialMedia.Core.Entities;
using SocialMedia.Core.Interafaces;
using SocialMedia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly SocialMediaContext _socialMediaContext;
        public CommentRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            var comments = await _socialMediaContext.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
           var comment = await _socialMediaContext.Comments.FirstOrDefaultAsync(x =>x.Id == id);
           return comment;
        }

        public async Task InsertComment(Comment comment)
        {
            _socialMediaContext.Comments.Add(comment);
            await _socialMediaContext.SaveChangesAsync();
        }

        public Task UpdateComment(Comment comment)
        {
           _socialMediaContext.Comments.Update(comment);
            return _socialMediaContext.SaveChangesAsync();
        }

        public Task DeleteComment(Comment comment)
        {
            _socialMediaContext.Comments.Remove(comment);
            return _socialMediaContext.SaveChangesAsync();
        }
    }
}
