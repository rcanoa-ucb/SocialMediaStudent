using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Core.Entities;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;           //INYECCION DE DEPENDENCIA, solo nos interesa el insertar con la base de datos, tenemos que llamar a las interfaces

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [HttpGet]

        public async Task<IActionResult> GetPosts() //DEVUELVE LOS Metodos HTTPS, no recibe parametros

        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")] //ESTOS SON CON PARAMETROS
        public async Task<IActionResult> GetPostById(int id) //DEVUELVE LOS Metodos HTTPS, no recibe parametros

        {
            var post = await _postRepository.GetPostByIdAsync(id);
            return Ok(post);
        }

        [HttpPost] 
        public async Task<IActionResult> InsertPost(Post newPost) //DEVUELVE LOS Metodos HTTPS, no recibe parametros

        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}", newPost);
        }

        [HttpPut] 
        public async Task<IActionResult> UpdatePost(Post post) //DEVUELVE LOS Metodos HTTPS, no recibe parametros

        {
            await _postRepository.UpdatePost(post);
            return NoContent();
        }

        [HttpDelete] 
        public async Task<IActionResult> DeletePost(Post post) //DEVUELVE LOS Metodos HTTPS, no recibe parametros

        {
            await _postRepository.DeletePost(post);
            return NoContent();
        }
        //Main
    }
}
