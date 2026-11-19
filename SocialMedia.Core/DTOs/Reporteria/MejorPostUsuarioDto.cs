using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.CustomEntities.Reporteria
{
    public class MejorPostUsuario
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int TotalComentarios { get; set; }
        public int PosicionEnUsuario { get; set; }
    }
}
