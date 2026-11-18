using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Queries
{
    public static class Reportes
    {
        // La consulta que ya tenías
        public static string autoComentaristasMySql = @"
            SELECT u.Id AS UserId,
                   CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                   u.Email,
                   COUNT(c.Id)                AS ComentariosPropios,
                   COUNT(DISTINCT c.PostId)   AS PostsAfectados
            FROM Users u
            INNER JOIN Posts p    ON p.UserId = u.Id
            INNER JOIN Comments c ON c.PostId = p.Id
            WHERE c.UserId = p.UserId
              AND c.IsActive = 1
            GROUP BY u.Id, u.FirstName, u.LastName, u.Email
            ORDER BY ComentariosPropios DESC, u.Id ASC
            LIMIT @Limit;";

        // ---- LA NUEVA CONSULTA PARA LA TAREA ----
        public static string rankingPostsPorUsuarioMySql = @"
            WITH RankingPosts AS (
                SELECT 
                    p.UserId,
                    CONCAT(u.FirstName, ' ', u.LastName) AS NombreUsuario,
                    p.Id AS PostId,
                    p.Description AS DescripcionPost,
                    COUNT(c.Id) AS TotalComentarios,
                    ROW_NUMBER() OVER (PARTITION BY p.UserId ORDER BY COUNT(c.Id) DESC, p.Id ASC) AS PosicionEnUsuario
                FROM Posts p
                INNER JOIN Users u ON u.Id = p.UserId
                INNER JOIN Comments c ON c.PostId = p.Id
                WHERE c.IsActive = 1
                GROUP BY p.UserId, u.FirstName, u.LastName, p.Id, p.Description
            )
            SELECT 
                UserId,
                NombreUsuario,
                PostId,
                DescripcionPost,
                TotalComentarios,
                PosicionEnUsuario
            FROM RankingPosts
            WHERE PosicionEnUsuario <= 3
            ORDER BY UserId, PosicionEnUsuario;";
    }
}