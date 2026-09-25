using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository //INYECCION LLAMAR AL PADRE
    {
        private readonly SocialMediaContext _socialMediaContext;        //SE LO INYECTA EN LA ENTRADA DE ALGO, POR EL METODO DEL CONSTRUCTOR //declarar la variable

        public CommentRepository(SocialMediaContext socialMediaContext)    //DENTRO DEL CONSTRUCTOR , LE ASIGNAS EL DATO DEL CONSTRUCTOR AL DE ARRIBA //poner al constructor, inyeccion al parametro
        {
            _socialMediaContext = socialMediaContext;                   //asignar al parametro del constructor
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()   //OBTENER TODOS LOS COMMENTS
        {
            var comments = await _socialMediaContext.Comments.ToListAsync();  //Es para modificar tus tablas de asincronico //SELECT * FROM Comments
            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)   //REGISTRO DE COMMENTS DE TODOS LOS IDS
        {
            var comment = await _socialMediaContext.Comments.
                FirstOrDefaultAsync(x => x.Id == id); //EL PRIMER VALOR QUE SE MUESTRA CON UNA CONDICIONAL x = variable temporal. Expresion lamda, buscar el primer elemento
            return comment;
        }

        public async Task InsertComment(Comment comment)  //ES VOID QUE SI NO TIENE NADA Task<Comment> solo Task,
                                                          //POR EJEMPLO SIN EL ASYNC, siempre cuando escribas await hay que colocar el async
        {
            _socialMediaContext.Comments.Add(comment);          //Es una transaccion para insertar, es todo lo que afecta a la base de datos o la estructura.
            await _socialMediaContext.SaveChangesAsync(); //es como cargar una bala y disparar
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