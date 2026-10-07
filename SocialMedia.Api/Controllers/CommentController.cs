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


        #region SinDtos

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

            if (comment == null)
            {
                return NotFound("Comentario no encontrado.");
            }

            return Ok(comment);
        }


        [HttpPost]
        public async Task<IActionResult> InsertComment(Comment newComment)
        {
            await _commentRepository.InsertComment(newComment);

            return Created(
                $"api/comment/{newComment.Id}",
                newComment);
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


        #region ConDtos

        [HttpGet("dto")]
        public async Task<IActionResult> GetCommentsDto()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();

            var commentDto = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                PostId = c.PostId,
                UserId = c.UserId,
                Description = c.Description,
                Date = c.Date,
                IsActive = c.IsActive,
                Post = c.Post,
                User = c.User
            });

            return Ok(commentDto);
        }


        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetCommentByIdDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound("Comentario no encontrado.");
            }

            var commentDto = new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Description = comment.Description,
                Date = comment.Date,
                IsActive = comment.IsActive,
                Post = comment.Post,
                User = comment.User
            };

            return Ok(commentDto);
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
                Post = newComment.Post,
                User = newComment.User
            };

            await _commentRepository.InsertComment(comment);

            return Created(
                $"api/comment/{comment.Id}",
                newComment);
        }


        [HttpPut("dto/{id}")]
        public async Task<IActionResult> UpdateCommentDto(
            int id,
            [FromBody] CommentDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest(
                    "El id del comentario no coincide.");
            }

            var comment = await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound(
                    "Comentario no encontrado.");
            }


            // Mapear valores del DTO a la entidad
            comment.PostId = commentDto.PostId;
            comment.UserId = commentDto.UserId;
            comment.Description = commentDto.Description;
            comment.Date = commentDto.Date;
            comment.IsActive = commentDto.IsActive;
            comment.Post = commentDto.Post;
            comment.User = commentDto.User;


            await _commentRepository.UpdateComment(comment);

            return Ok(comment);
        }


        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteCommentDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound(
                    "Comentario no encontrado.");
            }

            await _commentRepository.DeleteComment(comment);

            return NoContent();
        }

        #endregion


        #region Dto-AutoMapper

        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetCommentsDtoMapper()
        {
            var comments =
                await _commentRepository.GetAllCommentsAsync();

            var commentDto =
                _mapper.Map<IEnumerable<CommentDto>>(comments);

            return Ok(commentDto);
        }


        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetCommentByIdDtoMapper(int id)
        {
            var comment =
                await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound(
                    "Comentario no encontrado.");
            }

            var commentDto =
                _mapper.Map<CommentDto>(comment);

            return Ok(commentDto);
        }


        [HttpPost("dto/mapper")]
        public async Task<IActionResult> InsertCommentDtoMapper(
            [FromBody] CommentDto newComment)
        {
            if (newComment == null)
            {
                return BadRequest(
                    "Los datos del comentario son obligatorios.");
            }

            // DTO -> Entity
            var comment =
                _mapper.Map<Comment>(newComment);

            await _commentRepository.InsertComment(comment);

            // Entity -> DTO
            var commentDto =
                _mapper.Map<CommentDto>(comment);

            return Created(
                $"api/comment/dto/mapper/{comment.Id}",
                commentDto);
        }


        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateCommentDtoMapper(
            int id,
            [FromBody] CommentDto commentDto)
        {
            if (commentDto == null)
            {
                return BadRequest(
                    "Los datos del comentario son obligatorios.");
            }

            if (id != commentDto.Id)
            {
                return BadRequest(
                    "El id de la URL no coincide con el id del comentario.");
            }

            var comment =
                await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound(
                    "Comentario no encontrado.");
            }


            // DTO -> Entity
            _mapper.Map(commentDto, comment);


            await _commentRepository.UpdateComment(comment);


            // Entity -> DTO
            var resultDto =
                _mapper.Map<CommentDto>(comment);

            return Ok(resultDto);
        }


        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteCommentDtoMapper(int id)
        {
            var comment =
                await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound(
                    "Comentario no encontrado.");
            }

            await _commentRepository.DeleteComment(comment);

            return NoContent();
        }

        #endregion
    }
}