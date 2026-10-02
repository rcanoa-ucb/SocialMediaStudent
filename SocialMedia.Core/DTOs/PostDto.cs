using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs
{
    public class PostDto // es propio esta clase no es de entitie framework
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; } = null!;

        public string? Imagen { get; set; }
    }
}
