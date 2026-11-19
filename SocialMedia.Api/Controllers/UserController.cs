using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
namespace SocialMedia.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _work;
    public UserController(IUnitOfWork work) => _work = work;
    [HttpGet]
    public async Task<IActionResult> Get() => Ok((await _work.Users.GetAllAsync()).Select(u => new
    { u.Id, u.FirstName, u.LastName, u.Email, u.IsActive }));
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
            return BadRequest("Nombre, apellido y correo son obligatorios.");
        var user = new User { FirstName = dto.FirstName, LastName = dto.LastName,
            Email = dto.Email, DateOfBirth = dto.DateOfBirth, Telephone = dto.Telephone };
        await _work.Users.AddAsync(user);
        await _work.SaveChangesAsync();
        return Created($"api/User/{user.Id}", new { user.Id, user.FirstName, user.LastName, user.Email });
    }
}
