using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase //hacer inyeccion a la interfaz
    {
        private readonly IPostRepository _postRepository;// no interesa saber que hace insertar, solo insetar sin saber
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        //region para ocualtar cdogigo
        #region Sin DTOs
        [HttpGet]
        public async Task<IActionResult> GetPosts()// async aisncrinonco con task //IActionResult TITNE LOS DATOS HTTP 
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            return Ok(post);
        }


        [HttpPost]
        public async Task<IActionResult> InsertPost(Post newPost)
        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}", newPost); // mostrar el id que creo //concatenacion de cadenas 
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            await _postRepository.UpdatePost(post);
            return Ok(post);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(Post post)
        {
            await _postRepository.DeletePost(post);
            return Ok(true);
        }
        #endregion

        #region Con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetPostsDto()// async aisncrinonco con task //IActionResult TITNE LOS DATOS HTTP 
        {
            var posts = await _postRepository.GetAllPostsAsync();
            var postsDto = posts.Select(p => new PostDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Date = p.Date,
                Description = p.Description,
                Imagen = p.Imagen,
            });
            return Ok(postsDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetPostDto(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            var postDto = new PostDto
            {
                Id = post.Id,
                UserId = post.UserId,
                Date = post.Date,
                Description = post.Description,
                Imagen = post.Imagen,
            };
            return Ok(postDto);
        }


        [HttpPost("dto")]
        public async Task<IActionResult> InsertPostDto(Post newPost)
        {
            await _postRepository.InsertPost(newPost);
            return Created($"api/post/{newPost.Id}", newPost); // mostrar el id que creo //concatenacion de cadenas 
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdatePostDto(Post post)
        {
            await _postRepository.UpdatePost(post);
            return Ok(post);
        }

        [HttpDelete("dto")]
        public async Task<IActionResult> DeletePostDto(Post post)
        {
            await _postRepository.DeletePost(post);
            return Ok(true);
        }
        #endregion
    }
}
