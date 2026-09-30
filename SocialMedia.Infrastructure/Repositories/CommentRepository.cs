using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        // INI Inyeccion dependencia
        private readonly SocialMediaContext _socialMediaContext; //Parece que esto permite la conexion la BD

        public CommentRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
        }
        // FIN Inyeccion dependencia


        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            var comments = await _socialMediaContext.Comments.ToListAsync(); // Muestra todo (SELECT * FROM)
            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            var comment = await _socialMediaContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
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
