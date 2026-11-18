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


        public static string rankingPostUsuarioMySql = @"
    SELECT
        r.UserId,
        r.NombreCompleto,
        r.PostId,
        r.Description,
        r.TotalComentarios,
        r.PosicionEnUsuario
    FROM
    (
        SELECT
            u.Id AS UserId,
            CONCAT(u.FirstName, ' ', u.LastName) AS NombreCompleto,
            p.Id AS PostId,
            p.Description,
            COUNT(c.Id) AS TotalComentarios,
            ROW_NUMBER() OVER (
                PARTITION BY u.Id
                ORDER BY COUNT(c.Id) DESC, p.Id ASC
            ) AS PosicionEnUsuario
        FROM user u
        INNER JOIN post p
            ON p.UserId = u.Id
        INNER JOIN comment c
            ON c.PostId = p.Id
            AND c.IsActive = 1
        GROUP BY
            u.Id,
            u.FirstName,
            u.LastName,
            p.Id,
            p.Description
    ) r
    WHERE r.PosicionEnUsuario <= 3
    ORDER BY
        r.UserId ASC,
        r.PosicionEnUsuario ASC;";

    }


}
