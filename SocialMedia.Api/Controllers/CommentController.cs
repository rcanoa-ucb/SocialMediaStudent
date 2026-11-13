using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(IMapper mapper, ICommentService commentService)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        #region Dto-AutoMapper

        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetCommentsDtoMapper()
        {
            var comments = await _commentService.GetAllComments();
            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            var response = new ApiResponse<IEnumerable<CommentDto>>(commentsDto);

            return Ok(response);
        }

        [HttpGet("dto/mapper/dapper")]
        public async Task<IActionResult> GetCommentsDtoMapperDapper([FromQuery] int limit = 10)
        {
            var comments = await _commentService.GetAllCommentsDapperAsync(limit);
            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            var response = new ApiResponse<IEnumerable<CommentDto>>(commentsDto);

            return Ok(response);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetCommentByIdDtoMapper(int id)
        {
            var comment = await _commentService.GetComment(id);
            if (comment == null)
                return NotFound(new { message = "Comment no encontrado." });

            var commentDto = _mapper.Map<CommentDto>(comment);
            var response = new ApiResponse<CommentDto>(commentDto);

            return Ok(response);
        }

        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertCommentDtoMapper(CommentDto commentDto)
        {
            try
            {
                var comment = _mapper.Map<Comment>(commentDto);
                await _commentService.InsertComment(comment);

                var resultDto = _mapper.Map<CommentDto>(comment);
                return CreatedAtAction(nameof(GetCommentByIdDtoMapper),
                    new { id = comment.Id }, resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el comment", error = ex.Message });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateCommentDtoMapper(int id,
            [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
                return BadRequest("El ID del comment no coincide.");

            var comment = await _commentService.GetComment(id);
            if (comment == null)
                return NotFound(new { message = "Comment no encontrado." });

            try
            {
                _mapper.Map(commentDto, comment);
                await _commentService.UpdateComment(comment);

                var resultDto = _mapper.Map<CommentDto>(comment);
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el comment", error = ex.Message });
            }
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteCommentDtoMapper(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "El ID debe ser mayor que cero." });

            var comment = await _commentService.GetComment(id);
            if (comment == null)
                return NotFound(new { message = "Comment no encontrado." });

            try
            {
                await _commentService.DeleteComment(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el comment", error = ex.Message });
            }
        }

        #endregion
    }
}