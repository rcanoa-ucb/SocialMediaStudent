using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private readonly SocialMediaContext _socialMediaContext;

        private readonly IDapperContext _dapper;

        public CommentRepository(SocialMediaContext socialMediaContext, IDapperContext dapper)
            : base(socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
            _dapper = dapper;
        }

        public Task<IEnumerable<Comment>> GetAllCommentsDapperAsync(int limit = 10) =>
            _dapper.QueryAsync<Comment>(CommentQueries.List, new { Limit = limit });

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            var comments = await _socialMediaContext.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            var comment = await _socialMediaContext.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            return comment;
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

        public async Task DeleteComment(Comment comment)
        {
            _socialMediaContext.Comments.Remove(comment);
            await _socialMediaContext.SaveChangesAsync();
        }
    }
}