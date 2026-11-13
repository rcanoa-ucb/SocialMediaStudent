using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Services.Interfaces;
using SocialMedia.Services.Validators;

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

        #region Sin DTOs
        //[HttpGet]
        //public async Task<ActionResult> GetPosts()
        //{
        //    var posts = await _postRepository.GetAllPostsAsync();
        //    return Ok(posts);
        //}
        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetPostsById(int id)
        //{
        //    var posts = await _postRepository.GetPostByIdAsync(id);
        //    return Ok(posts);
        //}

        //[HttpPost]
        //public async Task<IActionResult> InsertPost(Post newPost)
        //{
        //    await _postRepository.InsertPost(newPost);
        //    return Created($"api/post/{newPost.Id}", newPost);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdatePost(Post Post)
        //{
        //    await _postRepository.InsertPost(Post);
        //    return NoContent();
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeletePost(Post Post)
        //{
        //    await _postRepository.DeletePost(Post);
        //    return NoContent();
        //}
        #endregion

        #region Con DTOs
        //[HttpGet("dto")]
        //public async Task<IActionResult> GetPostsDto()
        //{
        //    var posts = await _postRepository.GetAllPostsAsync();
        //    var postDto = posts.Select(p => new PostDto
        //    {
        //        Id = p.Id,
        //        UserId = p.UserId,
        //        Date = p.Date.ToString("dd-mm-yyyy"),
        //        Description = p.Description,
        //        Imagen = p.Image
        //    });
        //    return Ok(postDto);
        //}

        //[HttpGet("dto/{id}")]
        //public async Task<IActionResult> GetPostByIdDto(int id)
        //{
        //    var post = await _postRepository.GetPostByIdAsync(id);
        //    var postDto = new PostDto
        //    {
        //        Id = post.Id,
        //        UserId = post.UserId,
        //        Date = post.Date.ToString("dd-mm-yyyy"),
        //        Description = post.Description,
        //        Imagen = post.Image
        //    };
        //    return Ok(post);
        //}

        //[HttpPost("dto")]
        //public async Task<IActionResult> InsertPostDto(PostDto newPost)
        //{
        //    var post = new Post
        //    {
        //        Id = newPost.Id,
        //        UserId = newPost.UserId,
        //        Date = Convert.ToDateTime(newPost.Date),
        //        Description = newPost.Description,
        //        Image = newPost.Imagen
        //    };
            
        //    await _postRepository.InsertPost(post);
        //    return Created($"api/post/{newPost.Id}", newPost);
        //}

        //[HttpPut("dto")]
        //public async Task<IActionResult> UpdatePostDto(
        //    int id, [FromBody] PostDto postDto)
        //{
        //    if (id != postDto.Id)
        //    {
        //        return BadRequest("El id del post no coincide");
        //    }

        //    var post = await _postRepository.GetPostByIdAsync(id);
        //    if (post == null)
        //        return NotFound("Post no encontrado");

        //    //Mapear valor DTO en la entidad
        //    post.UserId = postDto.UserId;
        //    post.Date = Convert.ToDateTime(postDto.Date);
        //    post.Description = postDto.Description;
        //    post.Image = postDto.Imagen;

        //    await _postRepository.UpdatePost(post);
        //    return Ok(post);
        //}

        //[HttpDelete("dto/{id}")]
        //public async Task<IActionResult> DeletePostDto(int id)
        //{
        //    var post = await _postRepository.GetPostByIdAsync(id);
        //    if (post == null)
        //        return NotFound("Post no encontrado.");

        //    await _postRepository.DeletePost(post);

        //    return NoContent(); // 204 sin contenido
        //}
        #endregion

        #region Dto-AutoMapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetPostsDtoMapper(
            [FromQuery] PostQueryFilters? filters)
        {
            var posts = await _postService.GetAllPostsAsync(filters);
            var postDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            //var postDto = posts.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    UserId = p.UserId,
            //    Date = p.Date,
            //    Description = p.Description,
            //    Imagen = p.Imagen
            //});

            var response = new ApiResponse
                <IEnumerable<PostDto>>(postDto);

            return Ok(response);
        }

        [HttpGet("dto/mapper/dapper")]
        public async Task<IActionResult> GetPostsDtoMapperDapper([FromQuery] int limit = 10)
        {
            var posts = await _postService.GetAllPostsDapperAsync(limit);
            var postDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            var response = new ApiResponse
                <IEnumerable<PostDto>>(postDto);

            return Ok(response);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetPostByIdDtoMapper(int id)
        {
            var post = await _postService.GetPostsByIdAsync(id);
            var postDto = _mapper.Map<PostDto>(post);
            //var postDto = new PostDto
            //{
            //    Id = post.Id,
            //    UserId = post.UserId,
            //    Date = post.Date,
            //    Description = post.Description,
            //    Imagen = post.Imagen
            //};

            var response = new ApiResponse
                <PostDto>(postDto);

            return Ok(response);
        }

        [HttpPost("dto/mapper/")]
        public async Task<IActionResult> InsertPostDtoMapper(PostDto postDto)
        {
            // Validar el DTO
            var validationResult = await _crearValidator.ValidateAsync(postDto);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(message: "Error de validación", details: validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    error = e.ErrorMessage
                }));
                //return BadRequest(new
                //{
                //    message = "Error de validación",
                //    errors = validationResult.Errors.Select(e => new
                //    {
                //        field = e.PropertyName,
                //        error = e.ErrorMessage
                //    })
                //});
            }

            try
            {
                var post = _mapper.Map<Post>(postDto);
                await _postService.InsertPost(post);

                var resultDto = _mapper.Map<PostDto>(post);
                return CreatedAtAction(nameof(GetPostByIdDtoMapper), new { id = post.Id }, resultDto);
            }
            catch (BusinessException)
            {
                // Re-lanzamos para que el Middleware genere la respuesta estructurada
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el post", error = ex.Message });
            }
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
        public async Task<IActionResult> GetPostByIdDtoMapper(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            var postDto = _mapper.Map<PostDto>(post);
            //var postDto = new PostDto
            //{
            //    Id = post.Id,
            //    UserId = post.UserId,
            //    Date = post.Date,
            //    Description = post.Description,
            //    Imagen = post.Imagen
            //};
            return Ok(post);
        }
        #endregion
    }
}