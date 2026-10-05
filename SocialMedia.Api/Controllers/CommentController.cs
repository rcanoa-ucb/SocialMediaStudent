using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.ComponentModel;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _CommentRepository;
        private readonly IMapper _mapper;
        public CommentController(IMapper mapper, ICommentRepository CommentRepository)
        {
            _CommentRepository = CommentRepository;
            _mapper = mapper;
        }

        #region Sin DTOs
        [HttpGet]
        public async Task<ActionResult> GetComments()
        {
            var Comments = await _CommentRepository.GetAllCommentsAsync();
            return Ok(Comments);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCommentsById(int id)
        {
            var Comments = await _CommentRepository.GetCommentByIdAsync(id);
            return Ok(Comments);
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment(Comment newComment)
        {
            await _CommentRepository.InsertComment(newComment);
            return Created($"api/Comment/{newComment.Id}", newComment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(Comment Comment)
        {
            await _CommentRepository.InsertComment(Comment);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Comment Comment)
        {
            await _CommentRepository.DeleteComment(Comment);
            return NoContent();
        }
        #endregion

        #region Con DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetCommentsDto()
        {
            var Comments = await _CommentRepository.GetAllCommentsAsync();
            var CommentDto = Comments.Select(p => new CommentDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Date = p.Date,
                Description = p.Description
            });
            return Ok(CommentDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetCommentByIdDto(int id)
        {
            var Comment = await _CommentRepository.GetCommentByIdAsync(id);
            var CommentDto = new CommentDto
            {
                Id = Comment.Id,
                UserId = Comment.UserId,
                Date = Comment.Date,
                Description = Comment.Description
            };
            return Ok(Comment);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertCommentDto(CommentDto newComment)
        {
            var Comment = new Comment
            {
                Id = newComment.Id,
                UserId = newComment.UserId,
                Date = newComment.Date,
                Description = newComment.Description            };

            await _CommentRepository.InsertComment(Comment);
            return Created($"api/Comment/{newComment.Id}", newComment);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdateCommentDto(
     int id, [FromBody] CommentDto CommentDto)
        {
            if (id != CommentDto.Id)
            {
                return BadRequest("El id del Comment no coincide");
            }

            var Comment = await _CommentRepository.GetCommentByIdAsync(id);
            if (Comment == null)
                return NotFound("Comment no encontrado");

            //Mapear valor DTO en la entidad
            Comment.UserId = CommentDto.UserId;
            Comment.Date = CommentDto.Date;
            Comment.Description = CommentDto.Description;

            await _CommentRepository.UpdateComment(Comment);
            return Ok(Comment);
        }


        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteCommentDto(int id)
        {
            var Comment = await _CommentRepository.GetCommentByIdAsync(id);
            if (Comment == null)
                return NotFound("Comment no encontrado.");

            await _CommentRepository.DeleteComment(Comment);

            return NoContent(); // 204 sin contenido
        }
        #endregion
        #region Dto-AutoMapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetCommentsDtoMapper()
        {
            var Comments = await _CommentRepository.GetAllCommentsAsync();
            var CommentDto = _mapper.Map<IEnumerable<CommentDto>>(Comments);
            //var CommentDto = Comments.Select(p => new CommentDto
            //{
            //    Id = p.Id,
            //    UserId = p.UserId,
            //    Date = p.Date,
            //    Description = p.Description,
            //    Imagen = p.Imagen
            //});
            return Ok(CommentDto);
        }
        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetCommentByIdDtoMapper(int id)
        {
            var Comment = await _CommentRepository.GetCommentByIdAsync(id);
            var CommentDto = _mapper.Map<CommentDto>(Comment);
            //// var CommentDto = new CommentDto
            // /{
            //     Id = Comment.Id,
            //     UserId = Comment.UserId,
            //     Date = Comment.Date,
            //     Description = Comment.Description,
            //     Imagen = Comment.Imagen
            // };
            return Ok(Comment);
        }
        #endregion
    }
}

