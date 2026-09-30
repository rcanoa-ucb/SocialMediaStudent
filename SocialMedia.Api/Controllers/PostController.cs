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
        //postrepository se conecta con el context directo 
        private readonly IPostRepository _postRepository; //interfaz ya sabe usar los metodos
       public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts =  await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }
        [HttpGet("{id}")] //recibe como parametro el id
        public async Task<IActionResult> GetPosts(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            return Ok(post);
        }
        [HttpPost]
        public async Task<IActionResult> InsertPost(Post newPost)
        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post /{newPost.Id}", newPost);
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
    }
}
