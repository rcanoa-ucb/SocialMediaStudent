using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        public CommentController(IMapper mapper, ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }


        #region Sin DTOs
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
        #endregion

        #region Con DTOs
        [HttpGet("dto")]
        public async Task<ActionResult> GetCommentsDto()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentsDto = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                PostId = c.PostId,
                UserId = c.UserId,
                Description = c.Description,
                Date = c.Date,
                IsActive = c.IsActive,
            });
            return Ok(commentsDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<ActionResult> GetCommentByIdDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Description = comment.Description,
                Date = comment.Date,
                IsActive = comment.IsActive,
            };
            return Ok(comment);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertCommentDto(CommentDto newComment)
        {
            var comment = new Comment
            {
                Id = newComment.Id,
                PostId = newComment.PostId,
                UserId = newComment.UserId,
                Description = newComment.Description,
                Date = newComment.Date,
                IsActive = newComment.IsActive,
            };
            await _commentRepository.InsertComment(comment);
            return Created($"api/comment/{newComment.Id}", newComment);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdateCommentDto(int id, [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
                return BadRequest("Id del comment NO coincide");

            var comment = await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null)
                return NotFound("Comment NO encontrado");

            // Mapear valor DTO en la entidad
            comment.PostId = commentDto.PostId;
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
                return NotFound("Comment no encontrado.");

            await _commentRepository.DeleteComment(comment);

            return NoContent();
        }
        #endregion

        #region Dto-Automapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetCommentsDtoMapper()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            return Ok(commentDto);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetPostByIdDtoMapper(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(comment);
        }
        #endregion
    }
}