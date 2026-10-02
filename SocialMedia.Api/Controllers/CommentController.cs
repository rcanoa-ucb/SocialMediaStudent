using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;


        public CommentController(IMapper mapper,ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        #region MyRegion Sin DTOs

        [HttpGet]
        public async Task<IActionResult> GetComment()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost]

        public async Task<IActionResult> InsertComment(Comment newComment)
        {
            await _commentRepository.InsertComment(newComment);
            return Created($"api/Comment/{newComment.Id}", newComment);
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
        public async Task<IActionResult> GetCommentDto()
        {
            var comment = await _commentRepository.GetAllCommentsAsync();
            var commentDto = comment.Select(p => new CommentDto
            {
                Id = p.Id,                   
                PostId = p.PostId,
                Description = p.Description,
                Date = p.Date,
                IsActive = p.IsActive

            });
            return Ok(comment);
        }

        [HttpGet("dto/{id}")]

        public async Task<IActionResult> GetCommentByIdDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                Description = comment.Description,
                Date = comment.Date,
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
                Description = newComment.Description,
                Date = newComment.Date,
                IsActive = newComment.IsActive

            };

            await _commentRepository.InsertComment(comment);
            return Created($"api/comment/{newComment.Id}", newComment);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdateCommentDto(
            int id, [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest("El id del Comment no coincide");
            }

            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound("Comment no encontrado");
            }

            //Mapear valores DTO en la entidad
            comment.Id = commentDto.Id;
            comment.PostId = commentDto.PostId;
            comment.Description = commentDto.Description;
            comment.Date = commentDto.Date;
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
        public async Task<IActionResult> GetCommentDtoMapper()
        {
            var comment = await _commentRepository.GetAllCommentsAsync();
            var commentDto = _mapper.Map<CommentDto>(comment);
            //var commentDot = _mapper.Map<IEnumerable<CommentDto>>(comment);
            //var commentDto = comment.Select(p => new CommentDto
            //{
            //    Id = p.Id,
            //    UserId = p.UserId,
            //    Date = p.Date,
            //    Description = p.Description,
            //});
            return Ok(comment);
        }

        #endregion

    }
}
