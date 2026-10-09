using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase // hacer inyeccion a la interfaz
    {
        private readonly IPostRepository _postRepository; // no interesa saber que hace insertar, solo insetar sin saber
        private readonly IMapper _mapper;

        public PostController(IMapper mapper, IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        // region para ocultar codigo
        #region Sin DTOs
        [HttpGet]
        public async Task<ActionResult> GetPosts() // async aisncrinonco con task //IActionResult TITNE LOS DATOS HTTP 
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
            return Created($"api/post/{newPost.Id}", newPost); // mostrar el id que creo //concatenacion de cadenas 
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
        public async Task<IActionResult> GetPostsDto() // async aisncrinonco con task //IActionResult TITNE LOS DATOS HTTP 
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postDto = posts.Select(p => new PostDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Date = p.Date.ToString("dd-mm-yyyy"),
                Description = p.Description,
                Imagen = p.Image // p es Post (Image), PostDto es (Imagen)
            });
            return Ok(postDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetPostByIdDto(int id) // from route por defecto [fromQuery] ingresar 
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            var postDto = new PostDto
            {
                Id = post.Id,
                UserId = post.UserId,
                Date = post.Date.ToString("dd-mm-yyyy"),
                Description = post.Description,
                Imagen = post.Image // post es (Image), postDto es (Imagen)
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
                Date = Convert.ToDateTime (newPost.Date),
                Description = newPost.Description,
                Image = newPost.Imagen // newPost es PostDto (Imagen), post es Post (Image)
            };

            await _postRepository.InsertPost(post);
            return Created($"api/post/{newPost.Id}", newPost); // mostrar el id que creo //concatenacion de cadenas 
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdatePostDto(
            int id, [FromBody] PostDto postDto) // parametro mandara por el body (frombody)
        {
            if (id != postDto.Id)
            {
                return BadRequest("El id del post no coincide");
            }

            // post ya contiene los valores 
            var post = await _postRepository.GetPostByIdAsync(id); // verificar si en un registro existe o no (existe post?)
            if (post == null)
                return NotFound("Post no encontrado");

            // Mapear valor DTO en la entidad // id no se modifica
            post.UserId = postDto.UserId;
            post.Date = Convert.ToDateTime (postDto.Date);
            post.Description = postDto.Description;
            post.Image = postDto.Imagen; // post es (Image), postDto es (Imagen)

            await _postRepository.UpdatePost(post); // post se conceta a la base de datos y postDto recibe parametro
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
        public async Task<IActionResult> GetPostsDtoMapper()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postDto = _mapper.Map<IEnumerable<PostDto>>(posts); // equivale a lo comentado
            //var postDto = posts.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    UserId = p.UserId,
            //    Date = p.Date,
            //    Description = p.Description,
            //    Imagen = p.Image
            //});
            return Ok(postDto);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetPostByIdDtoMapper(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            var postDto = _mapper.Map<PostDto>(post); // destino origen 
            //var postDto = new PostDto
            //{
            //    Id = post.Id,
            //    UserId = post.UserId,
            //    Date = post.Date,
            //    Description = post.Description,
            //    Imagen = post.Image
            //};
            return Ok(post);
        }

        [HttpPost("dto/mapper/")]
        public async Task<IActionResult> InsertPostDtoMapper(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _postRepository.InsertPost(post);
            return Ok(post);
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdatePostDtoMapper(int id, [FromBody] PostDto postDto)
        {
            if (id != postDto.Id)
                return BadRequest("El ID del post no coincide.");

            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post no encontrado.");

            _mapper.Map(postDto, post);

            await _postRepository.UpdatePost(post);

            return Ok(post);
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeletePostDtoMapper(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Post no encontrado.");
            await _postRepository.DeletePost(post);
            return NoContent(); // 204 sin contenido
        }

        #endregion
    }
}