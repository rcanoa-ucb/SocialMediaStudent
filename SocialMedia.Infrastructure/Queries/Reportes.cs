using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Queries
{
    public class Reportes
    {
        public static string autoComentaristasMySql = @"
            SELECT u.Id AS UserId,
                   CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                   u.Email,
                   COUNT(c.Id)                AS ComentariosPropios,
                   COUNT(DISTINCT c.PostId)   AS PostsAfectados
            FROM user u
            INNER JOIN post p    ON p.UserId = u.Id
            INNER JOIN comment c ON c.PostId = p.Id
            WHERE c.UserId = p.UserId       -- el comentarista es el mismo dueño del post
              AND c.IsActive = 1
            GROUP BY u.Id, u.FirstName, u.LastName, u.Email
            ORDER BY ComentariosPropios DESC, u.Id ASC
            LIMIT @Limit;";
        
        public static string rankingUsuariosMySql = @"
            SELECT u.FirstName AS  Nombre , u.Id AS IdUser, COUNT(c.Id) AS Total_Comentarios , p.Id AS IdPost
            FROM user u
            INNER JOIN post p ON p.UserId = u.Id
            INNER JOIN comment c ON c.PostId = p.Id
            WHERE @u.id
            GROUP BY p.Id
            ORDER BY TOTAL_Comentarios DESC
            LIMIT 3";
    }

}
