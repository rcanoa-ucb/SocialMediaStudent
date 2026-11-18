using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs
{
    public class RankigUsuarioDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int TotalComentarios { get; set; }
    }
}
