using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Text;
>>>>>>> main

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
<<<<<<< HEAD
        private readonly SocialMediaContext _context;

        public CommentRepository(SocialMediaContext context)
        {
            _context = context;
=======
        private readonly SocialMediaContext _socialMediaContext;

        public CommentRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
>>>>>>> main
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
<<<<<<< HEAD
            return await _context.Comments.ToListAsync();
=======
            var comments = await _socialMediaContext.Comments.ToListAsync();
            return comments;
>>>>>>> main
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
<<<<<<< HEAD
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
=======
            var comment = await _socialMediaContext.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            return comment;
>>>>>>> main
        }

        public async Task InsertComment(Comment comment)
        {
<<<<<<< HEAD
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
=======
            _socialMediaContext.Comments.Add(comment);
            await _socialMediaContext.SaveChangesAsync();
>>>>>>> main
        }

        public async Task UpdateComment(Comment comment)
        {
<<<<<<< HEAD
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
=======
            _socialMediaContext.Comments.Update(comment);
            await _socialMediaContext.SaveChangesAsync();
>>>>>>> main
        }

        public async Task DeleteComment(Comment comment)
        {
<<<<<<< HEAD
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
=======
            _socialMediaContext.Comments.Remove(comment);
            await _socialMediaContext.SaveChangesAsync();
>>>>>>> main
        }
    }
}