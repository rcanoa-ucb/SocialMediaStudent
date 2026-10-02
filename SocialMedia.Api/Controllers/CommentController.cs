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
            await _commentRepository.InsertComment(newComment);
            return Created($"api/post/{newComment.Id}", newComment);
        }
        [HttpPut("dto")]
        public async Task<IActionResult> UpdateCommentDto(Comment comment)
        {
            await _commentRepository.UpdateComment(comment);
            return NoContent();//NoContent es un método que devuelve un resultado HTTP 204 (No Content) indicando que la solicitud se ha procesado correctamente, pero no hay contenido para devolver en la respuesta.
        }
        [HttpDelete("dto")]
        public async Task<IActionResult> DeleteCommentDto(Comment comment)
        {
            await _commentRepository.DeleteComment(comment);
            return NoContent();
        }
        #endregion
    }
}
