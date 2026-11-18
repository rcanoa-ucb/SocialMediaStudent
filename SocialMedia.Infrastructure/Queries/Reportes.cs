using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Queries
{
    public static class Reportes
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

        public static string rankingPostsUsuarioMySql = @"
            WITH RankingPosts AS (
                SELECT 
                    u.Id AS UserId,
                    CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                    ROW_NUMBER() OVER (
                        PARTITION BY u.Id 
                        ORDER BY COUNT(c.Id) DESC, p.Date DESC
                    ) AS PosicionEnUsuario,
                    p.Id AS PostId,
                    p.Description,
                    p.Date AS DatePost,
                    COUNT(c.Id) AS Comentarios
                FROM User u
                INNER JOIN Post p ON u.Id = p.UserId
                INNER JOIN Comment c ON p.Id = c.PostId
                GROUP BY 
                    u.Id, 
                    u.FirstName, 
                    u.LastName, 
                    p.Id, 
                    p.Description, 
                    p.Date
            )
            SELECT 
                UserId,
                NombreCompleto,
                PosicionEnUsuario,
                PostId,
                Description,
                DatePost,
                Comentarios
            FROM RankingPosts
            WHERE PosicionEnUsuario <= 3
            ORDER BY UserId ASC, PosicionEnUsuario ASC;";
    }
}
