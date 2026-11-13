using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Queries
{
    public static class Primero
    {
        public static string unoMySql = @"
        Select Id, UserId, Date, Description, Image
        from Post
        order by Date desc
        limit @Limit";
    }
}
