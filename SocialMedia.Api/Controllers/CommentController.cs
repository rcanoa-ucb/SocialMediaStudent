using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

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
        public async Task<ActionResult> GetCommentsById(int id)
        {
            var comments = await _commentRepository.GetCommentByIdAsync(id);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment(Comment newComment)
        {
            await _commentRepository.InsertComment(newComment);
            return Created($"api/comment/{newComment.Id}", newComment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(Comment Comment)
        {
            await _commentRepository.InsertComment(Comment);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Comment Comment)
        {
            await _commentRepository.DeleteComment(Comment);
            return NoContent();
        }
        #endregion

        #region Con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetCommentsDto()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDto = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                PostId = c.PostId,
                UserId = c.UserId,
                Date = c.Date,
                Description = c.Description,
                IsActive = c.IsActive
            });
            return Ok(commentDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetCommentByIdDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Date = comment.Date,
                Description = comment.Description,
                IsActive = comment.IsActive
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
                Date = newComment.Date,
                Description = newComment.Description,
                IsActive = newComment.IsActive
            };


            await _commentRepository.InsertComment(comment);
            return Created($"api/comment/{newComment.Id}", newComment);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdateCommentDto(int id, [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest("El id del comment no coincide");
            }

            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Comment no encontrado");

            //Mapear valor DTO en la entidad
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

            return NoContent(); // 204 sin contenido
        }
        #endregion
        #region Dto-AutoMapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetCommentsDtoMapper()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            //var commentDto = comments.Select(c => new CommentDto
            //{
            //    Id = c.Id,
            //    PostId = c.PostId,
            //    UserId = c.UserId,
            //    Date = c.Date,
            //    Description = c.Description,
            //    IsActive = c.IsActive
            //});
            return Ok(commentDto);
        }
        #endregion
    }
}