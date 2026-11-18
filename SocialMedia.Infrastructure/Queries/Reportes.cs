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
        public const string RankingPostsPorUsuarioMySql = @"
            WITH ConteoPorPost AS (
                SELECT
                    p.UserId,
                    p.Id AS PostId,
                    COUNT(c.Id) AS TotalComentarios
                FROM post p
                LEFT JOIN comment c
                    ON c.PostId = p.Id
                    AND c.IsActive = 1
                GROUP BY p.UserId, p.Id
            ),
            Ranking AS (
                SELECT
                    UserId,
                    PostId,
                    TotalComentarios,
                    ROW_NUMBER() OVER (
                        PARTITION BY UserId
                        ORDER BY TotalComentarios DESC, PostId ASC
                    ) AS PosicionEnUsuario,
                    MAX(TotalComentarios) OVER (
                        PARTITION BY UserId
                    ) AS MaxComentariosDelUsuario
                FROM ConteoPorPost
            )
            SELECT
                r.UserId,
                CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                r.PostId,
                p.Description AS DescripcionPost,
                r.PosicionEnUsuario,
                r.TotalComentarios
            FROM Ranking r
            INNER JOIN user u ON u.Id = r.UserId
            INNER JOIN post p ON p.Id = r.PostId
            WHERE r.PosicionEnUsuario <= 3
              AND r.MaxComentariosDelUsuario > 0
            ORDER BY r.UserId, r.PosicionEnUsuario;";
    }
}
