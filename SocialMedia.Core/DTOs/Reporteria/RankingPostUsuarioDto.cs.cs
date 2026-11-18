
namespace SocialMedia.Core.DTOs.Reporteria
{
    public class RankingPostUsuarioDto
    {
        public int UserId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int PostId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int PosicionEnUsuario { get; set; }
        public int TotalComentarios { get; set; }
    }
}
