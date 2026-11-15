using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs.Reporteria
{
    public class MejorPostUsuarioDto
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public int PostId { get; set; }
        public int PosicionEnUsuario { get; set; }
        public int TotalComentarios { get; set; }
    }
}
