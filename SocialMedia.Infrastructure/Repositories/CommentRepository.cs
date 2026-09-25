using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository  // INYECCION DE DEPENDENCIAS EN EL CONTROLADOR O CONTRATO
    {
        private readonly SocialMediaContext _socialMediaContext; // empieza en minuscula inyectar con _

        public CommentRepository(SocialMediaContext socialMediaContext)  
        {
            _socialMediaContext = socialMediaContext;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync() // task es un proceso asincrono 
        {
            // variable significativa // llamara a la base de datos y a la tabla
            var comments = await _socialMediaContext.Comments.ToListAsync(); // var acepta cualquier tipo de dato
            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {                                             // primer valor que se muestra en la condicion 
            var comment = await _socialMediaContext.Comments.FirstOrDefaultAsync
                (x => x.Id == id); // variable temporal (x) expresion lambda    
            return comment;
        }

        public async Task InsertComment(Comment comment) // task sin <> es void no devuelve nada 
        {
            _socialMediaContext.Comments.Add(comment); // transaccion es todo procedimiento que afecta a la base de datos o cambia la estructura
            await _socialMediaContext.SaveChangesAsync(); // SaveChangesAsync sinonimo de commit
        }   // await esperar a que termine una operación asíncrona antes de continuar con la siguiente línea

        public async Task UpdateComment(Comment comment) // siempre async antes del metodo
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