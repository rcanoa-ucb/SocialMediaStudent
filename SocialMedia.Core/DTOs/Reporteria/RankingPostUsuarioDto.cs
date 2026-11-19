using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs.Reporteria
{
    public class RankingPostUsuarioDto
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public int PostId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int TotalComentarios { get; set; }
        public int PosicionEnUsuario { get; set; }
    }
}