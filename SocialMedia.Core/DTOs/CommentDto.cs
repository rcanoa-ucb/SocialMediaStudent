using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Core.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PostId debe ser mayor a 0")]
        public int PostId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "UserId debe ser mayor a 0")]
        public int UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        public DateTime Date { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
