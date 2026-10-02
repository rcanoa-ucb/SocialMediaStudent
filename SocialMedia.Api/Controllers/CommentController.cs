using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController   : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentController(IMapper mapper, ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
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
            await _commentRepository.UpdateComment(Comment);
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
                UserId = c.UserId,
                Date = c.Date,
                Description = c.Description,
            });
            return Ok(commentDto);
        }

        [HttpGet("/dto/{id}")]
        public async Task<IActionResult> GetCommentByIdDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                Date = comment.Date,
                Description = comment.Description,
            };
            return Ok(commentDto);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertCommentDto(Comment newComment)
        {
            var comment = new Comment
            {
                Id = newComment.Id,
                UserId = newComment.UserId,
                Date = newComment.Date,
                Description = newComment.Description,
            };

            await _commentRepository.InsertComment(comment);
            return Created($"api/comment/{newComment.Id}", newComment);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdateCommentDto(
           int id, [FromBody] Comment commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest("El id no coincide");
            }
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound("Comentario no encontrado");
            }
            //mapear usuario
            comment.UserId = commentDto.UserId;
            comment.Date = commentDto.Date;
            comment.Description = commentDto.Description;
            await _commentRepository.UpdateComment(commentDto);
            return Ok(comment);
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteCommentDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound("Comentario no encontrado");
            }

            await _commentRepository.DeleteComment(comment);
            return NoContent();
        }
        #endregion
        #region Dto_AutoMapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetPostDtoMapper()
        {
            var posts = await _commentRepository.GetAllCommentsAsync();
            var postDto = _mapper.Map<PostDto>(posts);
            //var postDto = new PostDto
            //{
            //    Id = post.Id,
            //    UserId = post.UserId,
            //    Date = post.Date,
            //    Description = post.Description,
            //    Imagen = post.Imagen
            //};
            return Ok(postDto);
        }

        [HttpGet("/dto/mapper/{id}")]
        public async Task<IActionResult> GetPostByIdDtoMapper(int id)
        {
            var post = await _commentRepository.GetCommentByIdAsync(id);
            var postDto = _mapper.Map<PostDto>(post);
            return Ok(postDto);
        }
        #endregion
    }
}
