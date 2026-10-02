using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; } = null!;

        public DateTime Date { get; set; }

        public ulong IsActive { get; set; }
    }
}
