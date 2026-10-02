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
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentController(
            ICommentRepository commentRepository,
            IPostRepository postRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return Ok(_mapper.Map<IEnumerable<CommentDto>>(comments));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment(CommentDto newComment)
        {
            var error = await ValidateReferencesAsync(newComment);
            if (error != null)
                return BadRequest(error);

            var comment = _mapper.Map<Comment>(newComment); // CommentProfile ignora el Id
            if (comment.Date == default)
                comment.Date = DateTime.Now;

            await _commentRepository.InsertComment(comment);

            var result = _mapper.Map<CommentDto>(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, CommentDto commentDto)
        {
            if (id != commentDto.Id)
                return BadRequest("El id del comentario no coincide.");

            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

            var error = await ValidateReferencesAsync(commentDto);
            if (error != null)
                return BadRequest(error);

            _mapper.Map(commentDto, comment); // copia valores del DTO (sin tocar el Id)

            await _commentRepository.UpdateComment(comment);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Comentario no encontrado.");

            await _commentRepository.DeleteComment(comment);
            return NoContent();
        }

        // Evita errores 500 por FK: valida que el post y el usuario existan
        private async Task<string?> ValidateReferencesAsync(CommentDto dto)
        {
            if (await _postRepository.GetPostByIdAsync(dto.PostId) == null)
                return $"El post {dto.PostId} no existe.";
            if (await _userRepository.GetUserByIdAsync(dto.UserId) == null)
                return $"El usuario {dto.UserId} no existe.";
            return null;
        }
    }
}
