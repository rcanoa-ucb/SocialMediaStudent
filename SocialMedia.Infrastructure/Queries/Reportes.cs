using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Queries
{
    public static class Reportes
    {   public static string autoComentaristasMySql = @"
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

        public static string postsPopularesPorUsuarioMySql = @"
            WITH PostsPorUsuario AS (
                SELECT
                    u.Id AS UserId,
                    CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                    p.Id AS PostId,
                    p.Description,
                    p.Date AS DatePost,
                    COUNT(c.Id) AS Comentarios,
                    ROW_NUMBER() OVER (
                        PARTITION BY u.Id
                        ORDER BY COUNT(c.Id) DESC, p.Id ASC
                    ) AS PosicionEnUsuario
                FROM user u
                INNER JOIN post p
                    ON p.UserId = u.Id
                INNER JOIN comment c
                    ON c.PostId = p.Id
                WHERE c.IsActive = 1
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
            FROM PostsPorUsuario
            WHERE PosicionEnUsuario <= 3
            ORDER BY UserId ASC, PosicionEnUsuario ASC;";

    }
}
