namespace SocialMedia.Core.DTOs.Reporteria
{
    public class RankingPostDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string DescripcionPost { get; set; } = string.Empty;
        public int TotalComentarios { get; set; }
        public int PosicionEnUsuario { get; set; }
    }
}