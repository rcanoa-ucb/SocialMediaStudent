
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Queries
{
    public static class Reportes
    {
        // REPORTE 1: AUTO COMENTARISTAS
        public static string autoComentaristasMySql = @"
            SELECT u.Id AS UserId,
                   CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                   u.Email,
                   COUNT(c.Id) AS ComentariosPropios,
                   COUNT(DISTINCT c.PostId) AS PostsAfectados
            FROM user u
            INNER JOIN post p ON p.UserId = u.Id
            INNER JOIN comment c ON c.PostId = p.Id
            WHERE c.UserId = p.UserId
              AND c.IsActive = 1
            GROUP BY u.Id, u.FirstName, u.LastName, u.Email
            ORDER BY ComentariosPropios DESC, u.Id ASC
            LIMIT @Limit;";

        // REPORTE 2: RANKING DE POSTS POR USUARIO
        public static string rankingPostUsuarioMySql = @"
            WITH RankingPosts AS
            (
                SELECT
                    u.Id AS UserId,
                    CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                    u.Email,
                    p.Id AS PostId,
                    p.Description,
                    COUNT(c.Id) AS TotalComentarios,
                    ROW_NUMBER() OVER (
                        PARTITION BY u.Id
                        ORDER BY COUNT(c.Id) DESC, p.Id ASC
                    ) AS PosicionEnUsuario
                FROM user u
                INNER JOIN post p ON p.UserId = u.Id
                INNER JOIN comment c ON c.PostId = p.Id
                WHERE c.IsActive = 1
                GROUP BY
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    p.Id,
                    p.Description
            )
            SELECT
                UserId,
                NombreCompleto,
                Email,
                PostId,
                Description,
                PosicionEnUsuario,
                TotalComentarios
            FROM RankingPosts
            WHERE PosicionEnUsuario <= @Limit
            ORDER BY UserId ASC, PosicionEnUsuario ASC;";
    }
}
