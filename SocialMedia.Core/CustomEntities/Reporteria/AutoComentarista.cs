using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.CustomEntities.Reporteria
{
    public class AutoComentarista
    {
        public int UserId { get; set; }
        public string NombreCompleto { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public int ComentariosPropios { get; set; }
      public int  PostAfectaddos { get; set; }   
    }
}
