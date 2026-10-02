using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using System.Runtime.InteropServices;

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
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        #region Sin DTOs
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
        #endregion
        #region Con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetPostsDto()// IActionResult es una interfaz que representa el resultado de una acción en un controlador. Es una forma de devolver diferentes tipos de respuestas HTTP desde un método de acción. Al usar IActionResult, puedes devolver diferentes tipos de resultados, como Ok(), NotFound(), BadRequest(), etc., dependiendo del resultado de la operación.
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postDto = posts.Select(p => new PostDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Description = p.Description,
                Imagen = p.Imagen
            });
            return Ok(postDto);
        }
        [HttpGet("dto/{id}")]//colocamos id para que se pueda obtener un post por su id, y el id se pasa como parámetro en la URL.
        public async Task<IActionResult> GetPostDtoById(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            var postDto = new PostDto
            {
                Id = post.Id,
                UserId = post.UserId,
                Description = post.Description,
                Imagen = post.Imagen
            };
            //if (post == null)
            //{
            //    return NotFound();
            //}
            return Ok(postDto);
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
            if (post == null)
                return NotFound("Post no encontrado");

            //Mapear valor DTO en la entidad
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
        //#region MyRegion
        //[HttpGet("dto/mapper")]
        //public async Task<IActionResult> GetPostsDtoMapper()// IActionResult es una interfaz que representa el resultado de una acción en un controlador. Es una forma de devolver diferentes tipos de respuestas HTTP desde un método de acción. Al usar IActionResult, puedes devolver diferentes tipos de resultados, como Ok(), NotFound(), BadRequest(), etc., dependiendo del resultado de la operación.
        //{
        //    var posts = await _postRepository.GetAllPostsAsync();
        //    var postDto = posts.Select(p => new PostDto
        //    {
        //        Id = p.Id,
        //        UserId = p.UserId,
        //        Description = p.Description,
        //        Imagen = p.Imagen
        //    });
        //    return Ok(postDto);
        //}
        //#endregion
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
        [HttpGet("dto/mapper{id}")]//colocamos id para que se pueda obtener un post por su id, y el id se pasa como parámetro en la URL.
        public async Task<IActionResult> GetPostByIdDtoMapper(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            //if (post == null)
            //{
            //    return NotFound();
            //}
            return Ok(post);
        }
        #endregion
    }
}
