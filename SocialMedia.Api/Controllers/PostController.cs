using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interafaces;
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
        public async Task<IActionResult> GetAllPosts()// IActionResult es una interfaz que representa el resultado de una acción en un controlador. Es una forma de devolver diferentes tipos de respuestas HTTP desde un método de acción. Al usar IActionResult, puedes devolver diferentes tipos de resultados, como Ok(), NotFound(), BadRequest(), etc., dependiendo del resultado de la operación.
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }
        [HttpGet("{id}")]//colocamos id para que se pueda obtener un post por su id, y el id se pasa como parámetro en la URL.
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            //if (post == null)
            //{
            //    return NotFound();
            //}
            return Ok(post);
        }
        [HttpPost]
        public async Task<IActionResult> InsertPost(Post newPost)
        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}", newPost);//Created es un método que devuelve un resultado HTTP 201 (Created) indicando que el recurso se ha creado correctamente. El primer parámetro es la URL del recurso recién creado, y el segundo parámetro es el objeto que representa el recurso creado.
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            await _postRepository.UpdatePost(post);
            return NoContent();//NoContent es un método que devuelve un resultado HTTP 204 (No Content) indicando que la solicitud se ha procesado correctamente, pero no hay contenido para devolver en la respuesta.
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePost(Post post)
        {
            await _postRepository.DeletePost(post);
            return NoContent();
        }
    }
}
