using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; } = null!;

        public DateTime Date { get; set; }

        public ulong IsActive { get; set; }

        public virtual Post Post { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public string Imagen { get; set; } = null!;
    }
}

