
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Services.Interfaces;
using SocialMedia.Services.Validators;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly CrearPostDtoValidator _crearValidator;

        public PostController(
            IPostService postService,
            IMapper mapper,
            CrearPostDtoValidator crearValidator)
        {
            _postService = postService;
            _mapper = mapper;
            _crearValidator = crearValidator;
        }

        // GET: api/Post/dto/mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetPostsDtoMapper(
            [FromQuery] PostQueryFilters? filters)
        {
            var posts = await _postService.GetAllPostsAsync(
                filters ?? new PostQueryFilters());

            var postDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            var response = new ApiResponse<IEnumerable<PostDto>>(postDto);

            return Ok(response);
        }

        // GET: api/Post/dto/mapper/dapper
        [HttpGet("dto/mapper/dapper")]
        public async Task<IActionResult> GetPostsDtoMapperDapper(
            [FromQuery] int limit = 10)
        {
            if (limit <= 0)
                limit = 10;

            var posts = await _postService.GetAllPostsDapperAsync(limit);

            var postDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            var response = new ApiResponse<IEnumerable<PostDto>>(postDto);

            return Ok(response);
        }

        // GET: api/Post/dto/mapper/5
        [HttpGet("dto/mapper/{id:int}")]
        public async Task<IActionResult> GetPostByIdDtoMapper(int id)
        {
            var post = await _postService.GetPostsByIdAsync(id);

            if (post == null)
                return NotFound("Post no encontrado.");

            var postDto = _mapper.Map<PostDto>(post);

            var response = new ApiResponse<PostDto>(postDto);

            return Ok(response);
        }

        // POST: api/Post/dto/mapper
        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertPostDtoMapper(
            [FromBody] PostDto postDto)
        {
            var validationResult =
                await _crearValidator.ValidateAsync(postDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var post = _mapper.Map<Post>(postDto);

            await _postService.InsertPost(post);

            var resultDto = _mapper.Map<PostDto>(post);

            return CreatedAtAction(
                nameof(GetPostByIdDtoMapper),
                new { id = post.Id },
                resultDto);
        }

        // POST: api/Post/dto
        [HttpPost("dto")]
        public Task<IActionResult> InsertPostDto(
            [FromBody] PostDto postDto)
        {
            return InsertPostDtoMapper(postDto);
        }

        // PUT: api/Post/dto?id=5
        [HttpPut("dto")]
        public async Task<IActionResult> UpdatePostDto(
            [FromQuery] int id,
            [FromBody] PostDto postDto)
        {
            if (id != postDto.Id)
                return BadRequest("El id del post no coincide.");

            if (!DateTime.TryParse(postDto.Date, out _))
                return BadRequest("La fecha no tiene un formato válido.");

            var post = await _postService.GetPostsByIdAsync(id);

            if (post == null)
                return NotFound("Post no encontrado.");

            _mapper.Map(postDto, post);

            await _postService.UpdatePost(post);

            var resultDto = _mapper.Map<PostDto>(post);

            return Ok(resultDto);
        }

        // DELETE: api/Post/dto/5
        [HttpDelete("dto/{id:int}")]
        public async Task<IActionResult> DeletePostDto(int id)
        {
            var post = await _postService.GetPostsByIdAsync(id);

            if (post == null)
                return NotFound("Post no encontrado.");

            await _postService.DeletePost(id);

            return NoContent();
        }
    }
}
