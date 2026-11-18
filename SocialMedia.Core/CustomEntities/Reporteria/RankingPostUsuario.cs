using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.CustomEntities.Reporteria
{
    public class RankingPostUsuario
    {
            public int UserId { get; set; }

            public string NombreCompleto { get; set; } = string.Empty;

            public int PostId { get; set; }

            public int TotalComentarios { get; set; }        
    }
}
