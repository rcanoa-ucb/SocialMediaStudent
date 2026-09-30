using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
=======
>>>>>>> 725b8d6a87a8877474b87826442167d3c2f85f95
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Core.Entities;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        #region Sin DTOs
        [HttpGet]
        public async Task<ActionResult> GetPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPostsById(int id)
        {
            var posts = await _postRepository.GetPostByIdAsync(id);
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPost(Post newPost)
        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}", newPost);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(Post Post)
        {
            await _postRepository.InsertPost(Post);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(Post Post)
        {
            await _postRepository.DeletePost(Post);
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
        public async Task<IActionResult> InsertPostDto(Post newPost)
        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}", newPost);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdatePostDto(Post post)
        {
            await _postRepository.UpdatePost(post);
            return NoContent();
        }

        [HttpDelete("dto")]
        public async Task<IActionResult> DeletePostDto(Post post)
        {
            await _postRepository.DeletePost(post);
            return NoContent();
        }
        #endregion
    }
}
