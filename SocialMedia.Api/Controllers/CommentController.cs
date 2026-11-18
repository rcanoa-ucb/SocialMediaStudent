using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Entities;
using AutoMapper;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Services.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository,
            ICommentService commentService, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet("dto/mapper/dapper")]
        public async Task<IActionResult> GetCommentsDtoMapperDapper([FromQuery] int limit = 10)
        {
            var comments = await _commentService.GetAllCommentsDapperAsync(limit);
            var commentDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            var response = new ApiResponse<IEnumerable<CommentDto>>(commentDto);

            return Ok(response);
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
    }
}
