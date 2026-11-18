namespace SocialMedia.Infrastructure.Queries;

public static class CommentQueries
{
    public const string List = """
        SELECT Id, PostId, UserId, Description, Date,
               CAST(IsActive AS UNSIGNED) AS IsActive
        FROM comment
        ORDER BY Date DESC, Id DESC
        LIMIT @Limit
        """;
}
