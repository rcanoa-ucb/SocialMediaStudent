using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetComments([FromQuery] CommentQueryFilters filters)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var comments = await _commentService.GetAllCommentsAsync(filters);
            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            return Ok(commentsDto);
        }

        [HttpGet("dapper")]
        public async Task<IActionResult> GetCommentsDapper([FromQuery] int limit = 10)
        {
            if (limit < 1)
            {
                return BadRequest();
            }

            var comments = await _commentService.GetAllCommentsDapperAsync(limit);
            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment(CommentDto CommentDto)
        {
            var newComment = _mapper.Map<Comment>(CommentDto);
            await _commentService.InsertComment(newComment);
            var resultDto = _mapper.Map<CommentDto>(newComment);
            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, CommentDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest();
            }

            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _mapper.Map(commentDto, comment);
            await _commentService.UpdateComment(comment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentService.DeleteComment(id);
            return NoContent();
        }
    }
}