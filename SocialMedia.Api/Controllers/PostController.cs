using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using SocialMedia.Core.DTOs;
using SocialMedia.Services.Interfaces;
namespace SocialMedia.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _service;
    public PostController(IPostService service) => _service = service;
    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var post = await _service.GetByIdAsync(id);
        return post is null ? NotFound() : Ok(post);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostDto dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ValidationException ex) { return BadRequest(ex.Errors.Select(x => x.ErrorMessage)); }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PostDto dto)
    {
        if (dto.Id != 0 && dto.Id != id) return BadRequest("El ID no coincide.");
        try { return await _service.UpdateAsync(id, dto) ? NoContent() : NotFound(); }
        catch (ValidationException ex) { return BadRequest(ex.Errors.Select(x => x.ErrorMessage)); }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await _service.DeleteAsync(id) ? NoContent() : NotFound();
}
