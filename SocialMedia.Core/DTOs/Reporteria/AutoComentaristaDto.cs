using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs.Reporteria
{
    public class AutoComentaristaDto
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ComentariosPropios { get; set; }
        public int PostsAfectados { get; set; }
    }

}
