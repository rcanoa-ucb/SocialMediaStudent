using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Entities;
using SocialMedia.Infrastructure.Repositories;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Core.DTOs;
using AutoMapper;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository; //para hacer la inyeccion por dependencias
        private readonly IMapper _mapper;

        public PostController(IMapper mapper,IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        #region MyRegion Sin DTOs

        [HttpGet]
        public async Task<IActionResult> GetPost()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            return Ok(post);
        }

        [HttpPost]

        public async Task<IActionResult> InsertPost(Post newPost)
        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}",newPost);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            await _postRepository.UpdatePost(post);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(Post post)
        {
            await _postRepository.DeletePost(post);
            return NoContent();
        }

        #endregion


        #region Con DTOs

        [HttpGet("dto")]
        public async Task<IActionResult> GetPostDto()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postDto = posts.Select(p => new PostDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Date = p.Date,
                Description = p.Description,
                Imagen=p.Imagen
            });
            return Ok(posts);
        }

        [HttpGet("dto/{id}")]

        public async Task<IActionResult> GetPostByIdDto(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            var postDto = new PostDto
            {
                Id = post.Id,
                UserId = post.UserId,
                Date = post.Date,
                Description = post.Description,
                Imagen = post.Imagen
            };
            return Ok(post);
        }

        [HttpPost("dto")]

        public async Task<IActionResult> InsertPostDto(PostDto newPost)
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
            return Created($"api/post/{newPost.Id}", newPost);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdatePostDto(
            int id, [FromBody] PostDto postDto)
        {
            if (id != postDto.Id)
            {
                return BadRequest("El id del post no coincide");
            }
                
            var post = await _postRepository.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound("Post no encontrado");
            }

            //Mapear valores DTO en la entidad
            post.Id = postDto.Id;
            post.UserId = postDto.UserId;
            post.Date = postDto.Date;
            post.Description = postDto.Description;
            post.Imagen = postDto.Imagen;

            await _postRepository.UpdatePost(post);
            return Ok(post);
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeletePostDto(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post no encontrado.");

            await _postRepository.DeletePost(post);

            return NoContent(); // 204 sin contenido
        }

        #endregion

        #region Dto-AutoMapper

        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetPostDtoMapper()
        {
            var post = await _postRepository.GetAllPostsAsync();
            var postDto = _mapper.Map<PostDto>(post);
            //var postDot = _mapper.Map<IEnumerable<PostDto>>(post);
            //var postDto = posts.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    UserId = p.UserId,
            //    Date = p.Date,
            //    Description = p.Description,
            //    Imagen = p.Imagen
            //});
            return Ok(post);
        }

        #endregion

    }
}


