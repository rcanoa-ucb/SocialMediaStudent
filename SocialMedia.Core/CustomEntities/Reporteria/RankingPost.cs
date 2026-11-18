namespace SocialMedia.Core.CustomEntities.Reporteria
{
    public class RankingPost
    {
        public int UserId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int PostId { get; set; }
        public string DescripcionPost { get; set; } = string.Empty;
        public int TotalComentarios { get; set; }
        public int PosicionEnUsuario { get; set; }
    }
}