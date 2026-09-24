using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Interafaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync(); // Método para obtener todos los comentarios
        Task<Comment> GetCommentByIdAsync(int id); // Método para obtener un comentario por su ID
        Task InsertComment(Comment comment); // Método para insertar un nuevo comentario
        Task UpdateComment(Comment comment); // Método para actualizar un comentario existente
        Task DeleteComment(Comment comment); // Método para eliminar un comentario

    }
}
