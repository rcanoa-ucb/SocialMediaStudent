namespace SocialMedia.Core.Entities;

public partial class Comment
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public bool IsActive { get; set; }

    // Navegaciones nulables: si no, ASP.NET las exige como [Required] al recibir la entidad por JSON
    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
