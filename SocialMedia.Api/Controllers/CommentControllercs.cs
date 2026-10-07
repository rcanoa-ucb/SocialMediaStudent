
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using AutoMapper;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment(Comment newComment)
        {
            await _commentRepository.InsertComment(newComment);
            return Created($"api/comment/{newComment.Id}", newComment);
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




        #region Con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetCommentsDto()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDto = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                UserId = c.UserId,
                Description = c.Description,
                Date = c.Date,
            });
            return Ok(commentDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetCommentByIdDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

            var commentDto = new CommentDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                Description = comment.Description,
                Date = comment.Date,
            };
            return Ok(commentDto);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertCommentDto(CommentDto newCommentDto)
        {
            var comment = new Comment
            {
                Id = newCommentDto.Id,
                UserId = newCommentDto.UserId,
                Description = newCommentDto.Description,
                Date = newCommentDto.Date,
            };

            await _commentRepository.InsertComment(comment);
            return Created($"api/comment/{newCommentDto.Id}", newCommentDto);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdateCommentDto(int id, [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest("El id del comentario no coincide");
            }

            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Comentario no encontrado");

            // Mapear valor DTO en la entidad
            comment.PostId = commentDto.Id;
            comment.UserId = commentDto.UserId;
            comment.Description = commentDto.Description;
            comment.Date = commentDto.Date;

            await _commentRepository.UpdateComment(comment);
            return Ok(comment);
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteCommentDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

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
            return Ok(commentDto);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetCommentByIdDtoMapper(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }
        #endregion

    }
}