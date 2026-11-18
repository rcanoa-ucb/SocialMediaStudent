namespace SocialMedia.Core.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = null!;
        public string Date { get; set; } = null!;
        public ulong IsActive { get; set; }
    }
}
