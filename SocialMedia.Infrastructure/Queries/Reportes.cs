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
            JOIN post p    ON p.UserId = u.Id
            JOIN comment c ON c.PostId = p.Id
            WHERE c.UserId = p.UserId       -- el comentarista es el mismo dueño del post
                AND c.IsActive = true
            GROUP BY u.Id, u.FirstName, u.LastName, u.Email
            ORDER BY ComentariosPropios DESC, u.Id ASC
            LIMIT @Limit;";

        public static string mejorPostUsuarioMySql = @"
            WITH RankingPosts AS 
            (
                SELECT
                    u.Id AS UserId,
                    CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
                    p.Id AS PostId,
                    COUNT(c.Id) AS TotalComentarios,
                    ROW_NUMBER() OVER 
                    (
                        PARTITION BY u.Id
                        ORDER BY COUNT(c.Id) DESC
                    ) AS PosicionEnUsuario
                FROM user u
                JOIN post p
                    ON p.UserId = u.Id
                JOIN comment c
                    ON c.PostId = p.Id
                WHERE c.IsActive = true
                GROUP BY u.Id, u.FirstName, u.LastName, p.Id
            )

            SELECT
                UserId,
                NombreCompleto,
                PostId,
                TotalComentarios,
                PosicionEnUsuario
            FROM RankingPosts
            WHERE PosicionEnUsuario <= 3
            ORDER BY UserId, PosicionEnUsuario;";
    }
}
