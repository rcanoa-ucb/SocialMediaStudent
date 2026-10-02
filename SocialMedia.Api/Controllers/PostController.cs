using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PostController(
            IMapper mapper,
            IPostRepository postRepository,
            IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #region Sin DTOs
        [HttpGet]
        public async Task<ActionResult> GetPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetPostsById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post no encontrado.");

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPost(Post newPost)
        {
            if (await _userRepository.GetUserByIdAsync(newPost.UserId) == null)
                return BadRequest($"El usuario {newPost.UserId} no existe.");

            // El Id lo genera la BD; se ignoran navegaciones enviadas por el cliente
            newPost.Id = 0;
            newPost.User = null;
            newPost.Comments = new List<Comment>();

            await _postRepository.InsertPost(newPost);
            return CreatedAtAction(nameof(GetPostsById), new { id = newPost.Id }, newPost);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePost(int id, Post postUpdate)
        {
            if (id != postUpdate.Id)
                return BadRequest("El id del post no coincide.");

            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post no encontrado.");

            if (await _userRepository.GetUserByIdAsync(postUpdate.UserId) == null)
                return BadRequest($"El usuario {postUpdate.UserId} no existe.");

            post.UserId = postUpdate.UserId;
            post.Date = postUpdate.Date;
            post.Description = postUpdate.Description;
            post.Imagen = postUpdate.Imagen;

            await _postRepository.UpdatePost(post);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post no encontrado.");

            await _postRepository.DeletePost(post);
            return NoContent();
        }
        #endregion

        #region Con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetPostsDto()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postDto = posts.Select(p => new PostDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Date = p.Date,
                Description = p.Description,
                Imagen = p.Imagen
            });
            return Ok(postDto);
        }

        [HttpGet("dto/{id:int}")]
        public async Task<IActionResult> GetPostByIdDto(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post no encontrado.");

            var postDto = new PostDto
            {
                Id = post.Id,
                UserId = post.UserId,
                Date = post.Date,
                Description = post.Description,
                Imagen = post.Imagen
            };
            return Ok(postDto);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertPostDto(PostDto newPost)
        {
            if (await _userRepository.GetUserByIdAsync(newPost.UserId) == null)
                return BadRequest($"El usuario {newPost.UserId} no existe.");

            var post = new Post
            {
                UserId = newPost.UserId,
                Date = newPost.Date,
                Description = newPost.Description,
                Imagen = newPost.Imagen
            };

            await _postRepository.InsertPost(post);

            newPost.Id = post.Id; // Id generado por la BD
            return CreatedAtAction(nameof(GetPostByIdDto), new { id = post.Id }, newPost);
        }

        [HttpPut("dto/{id:int}")]
        public async Task<IActionResult> UpdatePostDto(int id, [FromBody] PostDto postDto)
        {
            if (id != postDto.Id)
                return BadRequest("El id del post no coincide.");

            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post no encontrado.");

            if (await _userRepository.GetUserByIdAsync(postDto.UserId) == null)
                return BadRequest($"El usuario {postDto.UserId} no existe.");

            // Mapear valores del DTO en la entidad
            post.UserId = postDto.UserId;
            post.Date = postDto.Date;
            post.Description = postDto.Description;
            post.Imagen = postDto.Imagen;

            await _postRepository.UpdatePost(post);
            return Ok(postDto);
        }

        [HttpDelete("dto/{id:int}")]
        public async Task<IActionResult> DeletePostDto(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post no encontrado.");

            await _postRepository.DeletePost(post);
            return NoContent();
        }
        #endregion

        #region Dto-AutoMapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetPostsDtoMapper()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            return Ok(postDto);
        }

        [HttpGet("dto/mapper/{id:int}")]
        public async Task<IActionResult> GetPostByIdDtoMapper(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post no encontrado.");

            var postDto = _mapper.Map<PostDto>(post);
            return Ok(postDto);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertPostDtoMapper(PostDto newPost)
        {
            if (await _userRepository.GetUserByIdAsync(newPost.UserId) == null)
                return BadRequest($"El usuario {newPost.UserId} no existe.");

            var post = _mapper.Map<Post>(newPost); // PostProfile ignora el Id
            await _postRepository.InsertPost(post);

            var postDto = _mapper.Map<PostDto>(post);
            return CreatedAtAction(nameof(GetPostByIdDtoMapper), new { id = post.Id }, postDto);
        }
        #endregion
    }
}
