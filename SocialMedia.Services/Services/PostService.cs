using AutoMapper;
using FluentValidation;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;
namespace SocialMedia.Services.Services;
public class PostService : IPostService
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;
    private readonly IValidator<PostDto> _validator;
    public PostService(IUnitOfWork work, IMapper mapper, IValidator<PostDto> validator)
    { _work = work; _mapper = mapper; _validator = validator; }
    public async Task<IEnumerable<PostDto>> GetAllAsync() =>
        _mapper.Map<IEnumerable<PostDto>>(await _work.Posts.GetAllAsync());
    public async Task<PostDto?> GetByIdAsync(int id)
    {
        var post = await _work.Posts.GetByIdAsync(id);
        return post is null ? null : _mapper.Map<PostDto>(post);
    }
    public async Task<PostDto> CreateAsync(PostDto dto)
    {
        await _validator.ValidateAndThrowAsync(dto);
        if (await _work.Users.GetByIdAsync(dto.UserId) is null)
            throw new ArgumentException("El usuario no existe.");
        if (dto.Description.Contains("sexo", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("El contenido no está permitido por las reglas del ejercicio.");
        var post = _mapper.Map<Post>(dto);
        post.Id = 0;
        await _work.Posts.AddAsync(post);
        await _work.SaveChangesAsync();
        return _mapper.Map<PostDto>(post);
    }
    public async Task<bool> UpdateAsync(int id, PostDto dto)
    {
        await _validator.ValidateAndThrowAsync(dto);
        var post = await _work.Posts.GetByIdAsync(id);
        if (post is null) return false;
        if (await _work.Users.GetByIdAsync(dto.UserId) is null)
            throw new ArgumentException("El usuario no existe.");
        _mapper.Map(dto, post);
        post.Id = id;
        await _work.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var post = await _work.Posts.GetByIdAsync(id);
        if (post is null) return false;
        _work.Posts.Delete(post);
        await _work.SaveChangesAsync();
        return true;
    }
}
