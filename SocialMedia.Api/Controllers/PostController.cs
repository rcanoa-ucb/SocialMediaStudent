using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    { //injercion de dependencias

        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostController(IMapper mapper, IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        #region SinDTOs
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync(); // Se conecta a la base de datos
            return Ok(posts);
        }

        [HttpGet("{id}")] // va recibir un parametro que va recibir id y va buscarlo en la base de datos

        public async Task<IActionResult> GetPostById(int id)
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
        #region con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetPostsDto()
        {
            var posts = await _postRepository.GetAllPostsAsync(); // Se conecta a la base de datos
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

        [HttpGet("dto/{id}")] // va recibir un parametro que va recibir id y va buscarlo en la base de datos

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
        public async Task<IActionResult> UpdatePostDto(int id, [FromBody] PostDto postDto) // se puede poner otros from, para hacer una conbinacion 
        { // si no se pone nada se asume que es from roate, y no from query
            if (id != postDto.Id)
            {
                return BadRequest("El id del post no coincide");
            }
            var post = await _postRepository.GetPostByIdAsync(id); //reutiliza si un registro existe o no
            if (post == null)
            {
                return NotFound("Post no encontrado");
            } //mapear valores DTO en la entidad
            post.UserId = postDto.UserId; //post recupera de post id ya recupera los valores, ya contiene el tipo de dato post solo la modifica con el usuario enviado
            post.Date = postDto.Date;
            post.Description = postDto.Description;
            post.Imagen = postDto.Imagen;
            await _postRepository.UpdatePost(post);
            return NoContent();
        }

        [HttpDelete("dto")]
        public async Task<IActionResult> DeletePostDto(int id) //para eliminar no necesitamos todo el objeto 
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
        public async Task<IActionResult> GetPostByIdMapper(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            var postDto = _mapper.Map<PostDto>(post); // destino origen 
            //{
          //    Id = post.Id,
          //    UserId = post.UserId,
          //    Date = post.Date,
           //   Description = post.Description,
          //    Imagen = post.Imagen,
           //};
            return Ok(post);
        #endregion
        }
    }
}
