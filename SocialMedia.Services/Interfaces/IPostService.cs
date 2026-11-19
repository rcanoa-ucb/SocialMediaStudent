using SocialMedia.Core.DTOs;
namespace SocialMedia.Services.Interfaces;
public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllAsync();
    Task<PostDto?> GetByIdAsync(int id);
    Task<PostDto> CreateAsync(PostDto dto);
    Task<bool> UpdateAsync(int id, PostDto dto);
    Task<bool> DeleteAsync(int id);
}
