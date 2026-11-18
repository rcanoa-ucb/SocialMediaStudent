using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.CustomEntities.Reporteria
{
    public class RankingPostUsuario
    {
        public int UserId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int PosicionEnUsuario { get; set; }
        public int PostId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DatePost { get; set; }
        public int Comentarios { get; set; }
    }
}
