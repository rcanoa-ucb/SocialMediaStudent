using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Queries
{
    public static class Reportes
    {
        // ROW_NUMBER limits each user to three posts, with a stable tie-breaker.
        public const string topPostsPorUsuarioMySql = @"
            WITH Conteos AS (
                SELECT p.UserId, p.Id AS PostId, COUNT(c.Id) AS TotalComentarios
                FROM post p
                LEFT JOIN comment c ON c.PostId = p.Id
                GROUP BY p.UserId, p.Id
            ), Ranking AS (
                SELECT UserId, PostId, TotalComentarios,
                       SUM(TotalComentarios) OVER (PARTITION BY UserId) AS ComentariosDelUsuario,
                       ROW_NUMBER() OVER (
                           PARTITION BY UserId
                           ORDER BY TotalComentarios DESC, PostId ASC
                       ) AS PosicionEnUsuario
                FROM Conteos
            )
            SELECT r.UserId, CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                   r.PostId, r.PosicionEnUsuario, r.TotalComentarios
            FROM Ranking r
            INNER JOIN user u ON u.Id = r.UserId
            WHERE r.PosicionEnUsuario <= 3 AND r.ComentariosDelUsuario > 0
            ORDER BY r.UserId, r.PosicionEnUsuario;";

        public static string autoComentaristasMySql = @"
            SELECT u.Id AS UserId,
                   CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                   u.Email,
                   COUNT(c.Id)                AS ComentariosPropios,
                   COUNT(DISTINCT c.PostId)   AS PostsAfectados
            FROM user u
            INNER JOIN post p    ON p.UserId = u.Id
            INNER JOIN comment c ON c.PostId = p.Id
            WHERE c.UserId = p.UserId       -- el comentarista es el mismo dueÃ±o del post
              AND c.IsActive = 1
            GROUP BY u.Id, u.FirstName, u.LastName, u.Email
            ORDER BY ComentariosPropios DESC, u.Id ASC
            LIMIT @Limit;";
    }
}
