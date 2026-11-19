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

        public static string rankingPostsPorUsuarioMySql = @"
            WITH ComentariosPorPost AS (
                SELECT p.UserId, p.Id AS PostId, p.Description, p.Date,
                       COUNT(c.Id) AS TotalComentarios
                FROM post p
                INNER JOIN comment c ON c.PostId = p.Id
                WHERE c.IsActive = 1
                GROUP BY p.UserId, p.Id, p.Description, p.Date
            ),
            Ranking AS (
                SELECT cp.*,
                       ROW_NUMBER() OVER (PARTITION BY cp.UserId
                                          ORDER BY cp.TotalComentarios DESC, cp.PostId ASC) AS PosicionEnUsuario
                FROM ComentariosPorPost cp
            )
            SELECT u.Id AS UserId,
                   CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                   r.PostId, r.Description, r.Date,
                   r.TotalComentarios, r.PosicionEnUsuario
            FROM Ranking r
            INNER JOIN user u ON u.Id = r.UserId
            WHERE r.PosicionEnUsuario <= 3
            ORDER BY u.Id ASC, r.PosicionEnUsuario ASC;";
    }
}
