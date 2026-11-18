using System;
namespace SocialMedia.Core.DTOs;


public class CommentDto
{
public int Id { get; set; }
public int UserId { get; set; }
public int PostId { get; set; }
public string Description { get; set; } = null!;
public bool IsActive { get; set; }
public string Date { get; set; } = null!;
}

