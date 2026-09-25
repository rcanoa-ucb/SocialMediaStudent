using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Core.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;           //INYECCION DE DEPENDENCIA, solo nos interesa el insertar con la base de datos, tenemos que llamar a las interfaces

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments() //DEVUELVE LOS Metodos HTTPS, no recibe parametros
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")] //ESTOS SON CON PARAMETROS
        public async Task<IActionResult> GetCommentById(int id) //DEVUELVE LOS Metodos HTTPS, no recibe parametros
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment(Comment newComment) //DEVUELVE LOS Metodos HTTPS, no recibe parametros
        {
            await _commentRepository.InsertComment(newComment);
            return Created($"api/comment/{newComment.Id}", newComment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(Comment comment) //DEVUELVE LOS Metodos HTTPS, no recibe parametros
        {
            await _commentRepository.UpdateComment(comment);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Comment comment) //DEVUELVE LOS Metodos HTTPS, no recibe parametros
        {
            await _commentRepository.DeleteComment(comment);
            return NoContent();
        }
    }
}