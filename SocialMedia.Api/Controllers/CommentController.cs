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
        public async Task<IActionResult> GetComment()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIDAsync(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment(Comment newcomment)
        {
            await _commentRepository.InsertComment(newcomment);
            return Created($"api/comment/{newcomment.Id}", newcomment);
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

            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDto = comments.Select(p => new CommentDto
            {
                Id = p.Id,
                PostId = p.PostId,
                UserId = p.UserId,
                Date = p.Date,
                Description = p.Description,
                IsActive = p.IsActive,

            });
            return Ok(commentDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetCommentByIdDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIDAsync(id);
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Date = comment.Date,
                Description = comment.Description,
                IsActive = comment.IsActive,
            };
            return Ok(comment);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertCommentDto(Comment newComment)
        {
            var comment = new Comment
            {
                Id = newComment.Id,
                PostId = newComment.PostId,
                UserId = newComment.UserId,
                Date = newComment.Date,
                Description = newComment.Description,
                IsActive = newComment.IsActive,
            };
            await _commentRepository.InsertComment(newComment);
            return Created($"api/comment/{newComment.Id}", newComment);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdateCommentDto(int id, [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
                return BadRequest("El ide del comment no coincide");

            var comment = await _commentRepository.GetCommentByIDAsync(id);
            if (comment == null)
                return NotFound("comment no encontrado");

            //Mapear valores DTO en la entidad

            comment.UserId = commentDto.UserId;
            comment.Date = commentDto.Date;
            comment.Description = commentDto.Description;
            comment.PostId = commentDto.PostId;
            comment.IsActive = commentDto.IsActive;

            await _commentRepository.UpdateComment(comment);
            return Ok(comment);
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteCommentDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIDAsync(id);
            if (comment == null)
                return NotFound("comment no encontrado.");

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
            //var commentDto = comments.Select(p => new commentDto
            //{
            //    Id = p.Id,
            //    UserId = p.UserId,
            //    Date = p.Date,
            //    Description = p.Description,
            //    Imagen = p.Imagen
            //});
            return Ok(commentDto);
        }

        public async Task<IActionResult> GetCommentsByIdDtoMapper(int id)
        {
            var comment = await _commentRepository.GetCommentByIDAsync(id);
            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(comment);
        }
        #endregion
    }
}