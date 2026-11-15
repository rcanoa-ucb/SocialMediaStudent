using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs.Reporteria
{
    public class PostComentadoDto
    {
        public int PostId { get; set; }
        public string? Descripcion { get; set; }
        public int CantidadComentarios { get; set; }
    }
}
