namespace SocialMedia.Core.CustomEntities.Reporteria
{
    public class TopPostUsuario
    {
        public int UserId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int PostId { get; set; }
        public long PosicionEnUsuario { get; set; }
        public long TotalComentarios { get; set; }
    }
}
