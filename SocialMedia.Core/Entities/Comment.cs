namespace SocialMedia.Core.Entities;
public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool IsActive { get; set; } = true;
    public Post? Post { get; set; }
    public User? User { get; set; }
}
