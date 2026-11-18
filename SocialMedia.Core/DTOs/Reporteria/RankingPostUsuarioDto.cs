namespace SocialMedia.Core.DTOs.Reporteria
{
    public class RankingPostUsuarioDto
    {
        public int UserId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int PostId { get; set; }
        public string DescripcionPost { get; set; } = string.Empty;
        public int TotalComentarios { get; set; }
        public int PosicionEnUsuario { get; set; }
    }
}