using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.CustomEntities.Reporteria
{
    public class PostComentado
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? Descripcion { get; set; }
        public int CantidadComentarios { get; set; }
    }
}
