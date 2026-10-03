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
        private readonly IMapper _mapper;

        public PostController(
            IMapper mapper,
            IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        // =========================================================
        // CRUD SIN DTO
        // =========================================================

        #region Sin DTOs

        // GET: api/post
        [HttpGet]
        public async Task<ActionResult> GetPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();

            return Ok(posts);
        }

        // GET: api/post/1
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPostsById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound("Post no encontrado.");
            }

            return Ok(post);
        }

        // POST: api/post
        [HttpPost]
        public async Task<IActionResult> InsertPost(Post newPost)
        {
            await _postRepository.InsertPost(newPost);

            return Created(
                $"api/post/{newPost.Id}",
                newPost
            );
        }

        // PUT: api/post
        [HttpPut]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            // CORREGIDO:
            // Antes estaba llamando InsertPost(post)
            await _postRepository.UpdatePost(post);

            return NoContent();
        }

        // DELETE: api/post
        [HttpDelete]
        public async Task<IActionResult> DeletePost(Post post)
        {
            await _postRepository.DeletePost(post);

            return NoContent();
        }

        #endregion


        // =========================================================
        // CRUD CON DTO
        // =========================================================

        #region Con DTOs

        // GET: api/post/dto
        [HttpGet("dto")]
        public async Task<IActionResult> GetPostsDto()
        {
            var posts =
                await _postRepository.GetAllPostsAsync();

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

        // GET: api/post/dto/1
        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetPostByIdDto(int id)
        {
            var post =
                await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound("Post no encontrado.");
            }

            var postDto = new PostDto
            {
                Id = post.Id,
                UserId = post.UserId,
                Date = post.Date,
                Description = post.Description,
                Imagen = post.Imagen
            };

            // CORREGIDO:
            // Antes decía return Ok(post);
            return Ok(postDto);
        }

        // POST: api/post/dto
        [HttpPost("dto")]
        public async Task<IActionResult> InsertPostDto(
            PostDto newPost)
        {
            var post = new Post
            {
                Id = newPost.Id,
                UserId = newPost.UserId,
                Date = newPost.Date,
                Description = newPost.Description,
                Imagen = newPost.Imagen
            };

            await _postRepository.InsertPost(post);

            return Created(
                $"api/post/{newPost.Id}",
                newPost
            );
        }

        // PUT: api/post/dto?id=1
        [HttpPut("dto")]
        public async Task<IActionResult> UpdatePostDto(
            int id,
            [FromBody] PostDto postDto)
        {
            if (id != postDto.Id)
            {
                return BadRequest(
                    "El id del post no coincide"
                );
            }

            var post =
                await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound(
                    "Post no encontrado"
                );
            }

            // Mapear los valores del DTO
            // hacia la entidad Post
            post.UserId = postDto.UserId;
            post.Date = postDto.Date;
            post.Description = postDto.Description;
            post.Imagen = postDto.Imagen;

            await _postRepository.UpdatePost(post);

            return Ok(post);
        }

        // DELETE: api/post/dto/1
        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeletePostDto(int id)
        {
            var post =
                await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound(
                    "Post no encontrado."
                );
            }

            await _postRepository.DeletePost(post);

            return NoContent();
        }

        #endregion


        // =========================================================
        // DTO UTILIZANDO AUTOMAPPER
        // =========================================================

        #region Dto-AutoMapper

        // GET: api/post/dto/mapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult>
            GetPostsDtoMapper()
        {
            var posts =
                await _postRepository.GetAllPostsAsync();

            var postDto =
                _mapper.Map<IEnumerable<PostDto>>(posts);

            return Ok(postDto);
        }

        // GET: api/post/dto/mapper/1
        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult>
            GetPostByIdDtoMapper(int id)
        {
            var post =
                await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound(
                    "Post no encontrado."
                );
            }

            var postDto =
                _mapper.Map<PostDto>(post);

            // CORREGIDO:
            // Antes decía return Ok(post);
            return Ok(postDto);
        }

        #endregion
    }
}