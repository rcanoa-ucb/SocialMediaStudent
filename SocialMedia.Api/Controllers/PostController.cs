using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;

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
        [HttpGet]
        public async Task<IActionResult> GetPosts()//IActionResult tiene los estados http
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
        public async Task<IActionResult> InsertPost(Post newPost)//IActionResult tiene los estados http
        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}", newPost); //opcional el llenarlos
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(Post post)//IActionResult tiene los estados http
        {
            await _postRepository.UpdatePost(post);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePost(Post post)//IActionResult tiene los estados http
        {
            await _postRepository.DeletePost(post);
            return NoContent();
        }
    }
}
