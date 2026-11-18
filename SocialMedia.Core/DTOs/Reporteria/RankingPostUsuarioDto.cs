using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs.Reporteria
{
    public class RankingPostUsuarioDto
    {
        public int UserId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public long PosicionEnUsuario { get; set; }
        public int PostId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DatePost { get; set; }
        public long Comentarios { get; set; }
    }
}