namespace SocialMedia.Core.DTOs.Reporteria
{
    public class PostRankingUsuarioDto
    {
        public int UserId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int PosicionEnUsuario { get; set; }
        public int PostId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DatePost { get; set; }
        public int Comentarios { get; set; }
    }
}
