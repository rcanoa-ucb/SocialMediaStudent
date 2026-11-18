using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces; // add this to have access to the interfaces
using SocialMedia.Infrastructure.Repositories;
using System.Net.WebSockets;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IMapper mapper, IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        #region Con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetPostsDto()//action result give you back http codes
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

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetPostsbyIdDto(int id)
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
        public async Task<IActionResult> InsertPostDto(Post newPost)
        {
            var post = new Post
            {
                Id = newPost.Id,
                UserId = newPost.UserId,
                Date = newPost.Date,
                Description = newPost.Description,
                Imagen = newPost.Imagen
            };
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}", newPost);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdatePostDto(int id, [FromBody] PostDto postDto)
        {
            if(postDto.Id != id)
            {
                return BadRequest("ID de post no coincide");
            }
            var post = await _postRepository.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound("Post no encontrado");
            }

            // mapear valores DTO en la entidad

            post.UserId = postDto.UserId;
            post.Date = postDto.Date;
            post.Description = postDto.Description;
            post.Imagen = postDto.Imagen;

            await _postRepository.UpdatePost(post);
            return Ok(post);
        }

        [HttpDelete("dto")]
        public async Task<IActionResult> DeletePostDto(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound("Post not found");
            }

            await _postRepository.DeletePost(post);
            return NoContent(); // 204 no content
        }
        #endregion

        #region Sin DTOs
        [HttpGet]
        public async Task<IActionResult> GetPosts()//action result give you back http codes
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostsbyId(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPost(Post newPost)
        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}", newPost);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(Post newPost)
        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}", newPost);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(Post post)
        {
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
            //var postDto = posts.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    UserId = p.UserId,
            //    Date = p.Date,
            //    Description = p.Description,
            //    Imagen = p.Imagen
            //});
            return Ok(postDto);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetPostsbyIdDtoMapper(int id)
        {

            var post = await _postRepository.GetPostByIdAsync(id);
            var postdto = _mapper.Map<PostDto>(post);
            //var postDto = new PostDto
            //{
            //    Id = post.Id,
            //    UserId = post.UserId,
            //    Date = post.Date,
            //    Description = post.Description,
            //    Imagen = post.Imagen
            //};
            return Ok(post);
        }

        #endregion


    }
}
