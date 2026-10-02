using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public readonly IMapper _mapper;
        public CommentController(IMapper mapper, ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        #region Sin DTOs
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return Ok(comments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            //if (comment == null)
            //{
            //    return NotFound();
            //}
            return Ok(comment);
        }
        [HttpPost]
        public async Task<IActionResult> InsertComment(Comment comment)
        {
            await _commentRepository.InsertComment(comment);
            return Created($"api/comment/{comment.Id}", comment);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            await _commentRepository.UpdateComment(comment);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Comment comment)
        {
            await _commentRepository.DeleteComment(comment);
            return NoContent();
        }
        #endregion
        #region Con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetCommentsDto()
        {
            var comments= await _commentRepository.GetAllCommentsAsync();
            var commentsDto = comments.Select(p => new CommentDto
            {
                Id = p.Id,
                PostId = p.PostId,
                UserId = p.UserId,
                Description = p.Description,
                Date = DateTime.Now,
                IsActive = p.IsActive,
            });
            return Ok(commentsDto);
        }
        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetCommentDtoById(int id) { 
        var comment = await _commentRepository.GetCommentByIdAsync(id);
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Description = comment.Description,
                Date = DateTime.Now,
                IsActive = comment.IsActive,
            };
            return Ok(commentDto);
        }
        [HttpPost("dto")]
        public async Task<IActionResult> InsertCommentDto(Comment newComment)
        {
            var comment = new Comment
            {
                Id = newComment.Id,
                PostId = newComment.PostId,
                UserId = newComment.UserId,
                Description = newComment.Description,
                Date = DateTime.Now,
                IsActive = newComment.IsActive,
            };

            await _commentRepository.InsertComment(comment);
            return Created($"api/post/{newComment.Id}",newComment);
        }
        [HttpPut("dto")]
        public async Task<IActionResult> UpdateCommenttDto(int id, [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest("El id del post no coincide");
            }

            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Post no encontrado");

            //Mapear valor DTO en la entidad
            comment.UserId = commentDto.UserId;
            comment.Date = commentDto.Date;
            comment.Description = commentDto.Description;
            comment.IsActive = commentDto.IsActive;

            await _commentRepository.UpdateComment(comment);
            return Ok(comment);
        }
        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteCommentDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Post no encontrado.");

            await _commentRepository.DeleteComment(comment);

            return NoContent(); // 204 sin contenido
        }
        #endregion
        #region Dto-AutoMapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetCommentsDtoMapper()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            //var postDto = posts.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    UserId = p.UserId,
            //    Date = p.Date,
            //    Description = p.Description,
            //    Imagen = p.Imagen
            //});
            return Ok(commentDto);
        }
        [HttpGet("dto/mapper{id}")]//colocamos id para que se pueda obtener un post por su id, y el id se pasa como parámetro en la URL.
        public async Task<IActionResult> GetCommentByIdDtoMapper(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            //if (post == null)
            //{
            //    return NotFound();
            //}
            return Ok(comment);
        }
        #endregion
    }
}
