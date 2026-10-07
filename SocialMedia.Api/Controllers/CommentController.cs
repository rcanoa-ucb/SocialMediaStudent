using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase // inyección a la interfaz
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        #region sin Dto
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment(Comment newComment)
        {
            await _commentRepository.InsertComment(newComment);
            return Created($"api/comment/{newComment.Id}", newComment); // mostrar el id que creo
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            await _commentRepository.UpdateComment(comment);
            return Ok(comment);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Comment comment)
        {
            await _commentRepository.DeleteComment(comment);
            return Ok(true);
        }
        #endregion

        #region Con Dto
        [HttpGet("dto")]
        public async Task<IActionResult> GetCommentsDto() // async asincrono con task //IActionResult TIENE LOS DATOS HTTP
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDto = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                PostId = c.PostId,
                UserId = c.UserId,
                Date = c.Date,
                Description = c.Description
            });
            return Ok(commentDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetCommentByIdDto(int id) // from route por defecto [fromQuery] ingresar
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Date = comment.Date,
                Description = comment.Description
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
                Date = newComment.Date,
                Description = newComment.Description
            };

            await _commentRepository.InsertComment(comment);
            return Created($"api/comment/{newComment.Id}", newComment); // mostrar el id que creo //concatenacion de cadenas
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdateCommentDto(
            int id, [FromBody] CommentDto commentDto) // parametro mandara por el body (frombody)
        {
            if (id != commentDto.Id)
            {
                return BadRequest("El id del comentario no coincide");
            }

            // comment ya contiene los valores
            var comment = await _commentRepository.GetCommentByIdAsync(id); // verificar si en un registro existe o no (existe comment?)
            if (comment == null)
                return NotFound("Comentario no encontrado");

            // Mapear valor DTO en la entidad // id no se modifica
            comment.PostId = commentDto.PostId;
            comment.UserId = commentDto.UserId;
            comment.Date = commentDto.Date;
            comment.Description = commentDto.Description;

            await _commentRepository.UpdateComment(comment); // comment se conecta a la base de datos y commentDto recibe parametro
            return Ok(comment);
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteCommentDto(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

            await _commentRepository.DeleteComment(comment);

            return NoContent(); // 204 sin contenido
        }
        #endregion

        #region Dto-AutoMapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetCommentsDtoMapper()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentDto = _mapper.Map<IEnumerable<CommentDto>>(comments); // equivale a lo comentado
            //var commentDto = comments.Select(c => new CommentDto
            //{
            //    Id = c.Id,
            //    PostId = c.PostId,
            //    UserId = c.UserId,
            //    Date = c.Date,
            //    Description = c.Description
            //});
            return Ok(commentDto);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetCommentByIdDtoMapper(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            var commentDto = _mapper.Map<CommentDto>(comment); // destino origen
            //var commentDto = new CommentDto
            //{
            //    Id = comment.Id,
            //    PostId = comment.PostId,
            //    UserId = comment.UserId,
            //    Date = comment.Date,
            //    Description = comment.Description
            //};
            return Ok(commentDto);
        }
        #endregion

    }
}