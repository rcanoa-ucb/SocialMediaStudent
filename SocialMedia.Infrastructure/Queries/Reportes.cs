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
        public static string mejorPostUsuarioMySql = @"
            SELECT
                UserId,
                PostId,
                Description,
                TotalComentarios,
                PosicionEnUsuario
            FROM (
                SELECT
                    p.UserId,
                    p.Id AS PostId,
                    p.Description,
                    COUNT(c.Id) AS TotalComentarios,
                    ROW_NUMBER() OVER (
                        PARTITION BY p.UserId
                        ORDER BY COUNT(c.Id) DESC, p.Date DESC
                    ) AS PosicionEnUsuario
                FROM post p
                INNER JOIN comment c ON c.PostId = p.Id
                GROUP BY p.UserId, p.Id, p.Description, p.Date
            ) ranking
            WHERE PosicionEnUsuario <= 3
            ORDER BY UserId, PosicionEnUsuario
            LIMIT @Limit;";
    }
}
