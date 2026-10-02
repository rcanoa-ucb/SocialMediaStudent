using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly SocialMediaContext _socialMediaContext;

        public CommentRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _socialMediaContext.Comments.ToListAsync();
        }

        public async Task<Comment> GetComment(int id)
        {
            return await _socialMediaContext.Comments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertComment(Comment comment)
        {
            _socialMediaContext.Comments.Add(comment);
            await _socialMediaContext.SaveChangesAsync();
        }

        public async Task UpdateComment(Comment comment)
        {
            _socialMediaContext.Comments.Update(comment);
            await _socialMediaContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteComment(int id)
        {
            var currentComment = await GetComment(id);
            if (currentComment == null)
            {
                return false;
            }

            _socialMediaContext.Comments.Remove(currentComment);
            int rows = await _socialMediaContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}