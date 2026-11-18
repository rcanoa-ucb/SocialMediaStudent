using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs
{
    public class AutoComentaristaDto
    { //no existe user id
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ComentariosPropios { get; set; }
        public int PostAfectaddos { get; set; }
    }
}
