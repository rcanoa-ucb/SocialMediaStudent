using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs.Reporteria
{
    public class RankingPostUsuarioDto
    {
        public int UserId { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public int PostId { get; set; }

        public int TotalComentarios { get; set; }
    }
}
