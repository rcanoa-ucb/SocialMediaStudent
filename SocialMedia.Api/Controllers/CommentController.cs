using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class commentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public commentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Getcomments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetcommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIDAsync(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Insertcomment(Comment newcomment)
        {
            await _commentRepository.InsertComment(newcomment);
            return Created($"api/comment/{newcomment.Id}", newcomment);
        }

        [HttpPut]
        public async Task<IActionResult> Updatecomment(Comment comment)
        {
            await _commentRepository.UpdateComment(comment);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Deletecomment(Comment comment)
        {
            await _commentRepository.DeleteComment(comment);
            return NoContent();
        }
    }
}