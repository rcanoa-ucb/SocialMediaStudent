
namespace SocialMedia.Infrastructure.Queries.Reportes
{
    public static class Reportes
    {
        public static string rankingPostsPorUsuarioMySql = @"
            SELECT
                ranking.UserId,
                ranking.NombreCompleto,
                ranking.PosicionEnUsuario,
                ranking.PostId,
                ranking.Description,
                ranking.DatePost,
                ranking.Comentarios
            FROM
            (
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

                GROUP BY
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    p.Id,
                    p.Description,
                    p.Date
            ) AS ranking

            WHERE ranking.PosicionEnUsuario <= 3

            ORDER BY
                ranking.UserId,
                ranking.PosicionEnUsuario;
        ";
    }
}
