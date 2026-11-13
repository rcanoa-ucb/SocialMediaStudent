using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Queries
{
    public static class CommentQueries
    {
        public static string comentariosMySql = @"
        Select Id, PostId, UserId, Description, Date, IsActive
        from comment
        order by Date desc
        limit @Limit";
    }
}
