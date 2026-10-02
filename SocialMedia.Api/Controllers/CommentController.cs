using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetComments();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _commentRepository.GetComment(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment(Comment comment)
        {
            await _commentRepository.InsertComment(comment);
            return Ok(comment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            await _commentRepository.UpdateComment(comment);
            return Ok(comment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentRepository.DeleteComment(id);
            return Ok(result);
        }
    }
}