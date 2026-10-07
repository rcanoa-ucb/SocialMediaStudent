using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        #region Sin DTOs
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetComments();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _commentRepository.GetComment(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentRepository.DeleteComment(id);
            return Ok(result);
        }
        #endregion

        #region Con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetCommentsDto()
        {
            var comments = await _commentRepository.GetComments();
            var commentsDto = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                PostId = c.PostId,
                UserId = c.UserId,
                Description = c.Description,
                Date = c.Date
            });
            return Ok(commentsDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetCommentByIdDto(int id)
        {
            var comment = await _commentRepository.GetComment(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

            var commentDto = new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Description = comment.Description,
                Date = comment.Date
            };
            return Ok(commentDto);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertCommentDto([FromBody] CommentDto newCommentDto)
        {
            var comment = new Comment
            {
                Id = newCommentDto.Id,
                PostId = newCommentDto.PostId,
                UserId = newCommentDto.UserId,
                Description = newCommentDto.Description,
                Date = newCommentDto.Date
            };

            await _commentRepository.InsertComment(comment);
            return Created($"api/comment/{comment.Id}", newCommentDto);
        }

        [HttpPut("dto/{id}")]
        public async Task<IActionResult> UpdateCommentDto(int id, [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest("El id del comentario no coincide.");
            }

            var comment = await _commentRepository.GetComment(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

            comment.PostId = commentDto.PostId;
            comment.UserId = commentDto.UserId;
            comment.Description = commentDto.Description;
            comment.Date = commentDto.Date;

            await _commentRepository.UpdateComment(comment);
            return Ok(comment);
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteCommentDto(int id)
        {
            var result = await _commentRepository.DeleteComment(id);
            if (!result)
                return NotFound("Comentario no encontrado.");

            return NoContent();
        }
        #endregion

        #region Dt0-AutoMapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetCommentsDtoMapper()
        {
            var comments = await _commentRepository.GetComments();
            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            return Ok(commentsDto);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetCommentByIdDtoMapper(int id)
        {
            var comment = await _commentRepository.GetComment(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertCommentDtoMapper([FromBody] CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            await _commentRepository.InsertComment(comment);

            var resultDto = _mapper.Map<CommentDto>(comment);
            return Created($"api/comment/{resultDto.Id}", resultDto);
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateCommentDtoMapper(int id, [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
                return BadRequest("El id del comentario no coincide.");

            var comment = await _commentRepository.GetComment(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

            _mapper.Map(commentDto, comment);
            await _commentRepository.UpdateComment(comment);

            return Ok(commentDto);
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteCommentDtoMapper(int id)
        {
            var result = await _commentRepository.DeleteComment(id);
            if (!result)
                return NotFound("Comentario no encontrado.");

            return NoContent();
        }
        #endregion
    }
}