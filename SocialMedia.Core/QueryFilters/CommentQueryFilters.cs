namespace SocialMedia.Core.QueryFilters;

public class CommentQueryFilters
{
    public int? UserId { get; set; }
    public int? PostId { get; set; }
    public DateOnly? Date { get; set; }
    public string? Description { get; set; }
}
