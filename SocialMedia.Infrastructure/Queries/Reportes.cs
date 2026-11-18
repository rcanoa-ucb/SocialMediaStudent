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
        WITH TotalComentarios AS (
            SELECT
                c.PostId,
                COUNT(*) AS Comentarios
            FROM comment c
            GROUP BY c.PostId
        ),
        PostsOrdenados AS (
            SELECT
                u.Id AS UserId,
                CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                p.Id AS PostId,
                p.Description,
                p.Date AS DatePost,
                tc.Comentarios,
                ROW_NUMBER() OVER (
                    PARTITION BY u.Id
                    ORDER BY tc.Comentarios DESC, p.Id ASC
                ) AS PosicionEnUsuario
            FROM post p
            INNER JOIN user u ON p.UserId = u.Id
            INNER JOIN TotalComentarios tc ON tc.PostId = p.Id
        )
        SELECT
            UserId,
            NombreCompleto,
            PosicionEnUsuario,
            PostId,
            Description,
            DatePost,
            Comentarios
        FROM PostsOrdenados
        WHERE PosicionEnUsuario <= @Limit
        ORDER BY UserId ASC, PosicionEnUsuario ASC;";
    }
}
