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
        public string DescripcionPost { get; set; } = string.Empty;
        public long PosicionEnUsuario { get; set; }
        public long TotalComentarios { get; set; }
    }
}
