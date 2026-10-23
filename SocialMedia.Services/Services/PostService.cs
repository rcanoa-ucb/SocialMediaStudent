using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Services.Services
{
    public class PostService : IPostService
    {
        public readonly IPostRepository _postRepository;
        public readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        public async Task<Post> GetPostsByIdAsync(int id)
        {
            return await _postRepository.GetPostByIDAsync(id);
        }

        public async Task InsertPost(Post post)
        {
            //1. Solo se permite usuarios existentes
            var user = await _userRepository.GetUserByIDAsync(post.UserId);
            if (user == null)
                throw new Exception("El usuario no existe");
            //2. No se puede publicar malas palabras
            if (ContainsForbiddenContent(post.Description))
                throw new Exception("El contenido no es permitido");

            await _postRepository.InsertPost(post);
        }

        public async Task UpdatePost(Post post)
        {
            await _postRepository.UpdatePost(post);
        }

        public async Task DeletePost(Post post)
        {
            await _postRepository.DeletePost(post);
        }

        // Lista de palabras o expresiones no permitidas
        public readonly string[] ForbiddenWords =
        {
            "violencia",
            "odio",
            "grosería",
            "discriminación"
        };

        public bool ContainsForbiddenContent(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;

            foreach (var word in ForbiddenWords)
            {
                if (text.Contains(word, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
