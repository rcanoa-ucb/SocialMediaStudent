using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.QueryFilters
{
    public class PostQueryFilters
    {
        public string UserId { get; set; } = null!;

        public string Date { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
