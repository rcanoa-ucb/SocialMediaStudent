using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces; // add this to have access to the interfaces
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


    }
}
