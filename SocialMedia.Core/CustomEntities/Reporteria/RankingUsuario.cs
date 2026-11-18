using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.CustomEntities.Reporteria
{
    public class RankingUsuario
    {
        public int UserId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int TotalComentarios { get; set; }
        public int PostId { get; set; }
    }
}
