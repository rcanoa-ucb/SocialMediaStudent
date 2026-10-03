using SocialMedia.Core.Entities;
<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Text;
>>>>>>> main

namespace SocialMedia.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int id);
        Task InsertComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(Comment comment);
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> main
